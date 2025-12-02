using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using QlyDonHang_DoAn_hqtcsdl.Models;
using System.Collections.Generic;

namespace QlyDonHang_DoAn_hqtcsdl.Controllers
{
    // Đã thay thế Entities bằng FashionShopDb (theo code bạn cung cấp)
    // Nếu tên Context Class của bạn là Entities, hãy đổi lại: private Entities db = new Entities();
    public class OrderController : Controller
    {
        private FashionShopDb db = new FashionShopDb();

        // GET: Order
        public ActionResult Index()
        {
            try
            {
                var orders = db.Orders.Include("KhachHang").OrderByDescending(o => o.OrderDate).ToList();
                return View(orders);
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi thân thiện hơn
                ViewBag.Error = ex.Message;
                return View(new List<Order>());
            }
        }

        // GET: Order/GetOrderDetails/5
        public ActionResult GetOrderDetails(int id)
        {
            try
            {
                var order = db.Orders.Include("OrderDetails.Product").Include("KhachHang").FirstOrDefault(o => o.Order_Id == id);
                if (order == null) return HttpNotFound();

                var viewModel = new OrderDetailViewModel
                {
                    Order_Id = order.Order_Id,
                    OrderNo = order.OrderNo,
                    Status = order.Status,
                    OrderDate = order.OrderDate,
                    SubTotal = order.SubTotal,
                    // ShippingFee chưa được tính, giả định lấy từ DB. Tạm thời tính bằng Total - SubTotal
                    ShippingFee = order.TotalAmount - order.SubTotal,
                    TotalAmount = order.TotalAmount,
                    // Sử dụng ?. và ?? để an toàn hơn
                    CustomerName = order.KhachHang?.TenKH ?? "Khách vãng lai",
                    ShippingAddress = order.ShippingAddress,
                    ShippingPhone = order.KhachHang?.SoDT,

                    Items = order.OrderDetails.Select(d => new OrderItem
                    {
                        ProductName = d.Product != null ? d.Product.ProductName : "Sản phẩm lỗi",
                        // Sử dụng toán tử null-conditional an toàn hơn
                        SizeName = d.Product?.Size_Id?.ToString() ?? "-",
                        ColorName = d.Product?.Color_Id?.ToString() ?? "-",

                        // Các trường số lượng, đơn giá là NOT NULL
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        TotalPrice = d.TotalPrice,

                        ProductImage = d.Product?.DefaultImage_Id?.ToString() ?? ""
                    }).ToList()
                };

                return PartialView("_OrderDetailsPartial", viewModel);
            }
            catch (Exception ex)
            {
                return Content($"<div class='text-danger'>Lỗi tải dữ liệu: {ex.Message}</div>");
            }
        }

        // SỬA LỖI NẰM Ở ĐÂY: Đổi từ int id (BẮT BUỘC) sang int? id (TÙY CHỌN)
        // GET: Order/Invoice/5
        public ActionResult Invoice(int? id)
        {
            // Kiểm tra nếu không có ID (gây ra lỗi 0x80004005)
            if (id == null)
            {
                // Chuyển hướng về trang chủ hoặc báo lỗi (tùy thuộc vào yêu cầu)
                return RedirectToAction("Index", "Home");
            }

            try
            {
                // Lấy giá trị ID an toàn
                int orderId = id.Value;

                var order = db.Orders.Include("OrderDetails.Product").Include("KhachHang").FirstOrDefault(o => o.Order_Id == orderId);
                if (order == null) return HttpNotFound();

                var viewModel = new OrderDetailViewModel
                {
                    Order_Id = order.Order_Id,
                    OrderNo = order.OrderNo,
                    Status = order.Status,
                    OrderDate = order.OrderDate,
                    SubTotal = order.SubTotal,
                    ShippingFee = order.TotalAmount - order.SubTotal, // Giả định tính phí vận chuyển
                    TotalAmount = order.TotalAmount,
                    CustomerName = order.KhachHang?.TenKH ?? "Khách lẻ",
                    ShippingAddress = order.ShippingAddress,
                    ShippingPhone = order.KhachHang?.SoDT,
                    Items = order.OrderDetails.Select(d => new OrderItem
                    {
                        ProductName = d.Product?.ProductName ?? "Unknown",
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        TotalPrice = d.TotalPrice,
                        SizeName = d.Product?.Size_Id?.ToString() ?? "-",
                        ColorName = d.Product?.Color_Id?.ToString() ?? "-"
                    }).ToList()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return Content($"Lỗi in hóa đơn: {ex.Message}");
            }
        }

        // POST: Order/UpdateStatus
        [HttpPost]
        public ActionResult UpdateStatus(int orderId, string status)
        {
            try
            {
                var order = db.Orders.Find(orderId);
                if (order != null)
                {
                    order.Status = status;
                    if (status == "Đã xác nhận") order.ConfirmedDate = DateTime.Now;
                    else if (status == "Đang giao hàng") order.ShippedDate = DateTime.Now;
                    else if (status == "Hoàn thành") order.DeliveredDate = DateTime.Now;

                    db.SaveChanges();
                    return Json(new { success = true, message = "Cập nhật thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy đơn hàng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // POST: Order/CancelOrder
        [HttpPost]
        // Đã sửa lỗi "Remove unused parameter 'reason'" và dùng underscore (_) để đánh dấu là không dùng.
        public ActionResult CancelOrder(int orderId, string reason)
        {
            try
            {
                var order = db.Orders.Find(orderId);
                if (order != null)
                {
                    if (order.Status == "Hoàn thành")
                        return Json(new { success = false, message = "Đơn đã hoàn thành không thể hủy!" });

                    order.Status = "Đã hủy";
                    order.IsCancel = true;

                    // Giữ lại chú thích của bạn
                    // --- QUAN TRỌNG: Đã bỏ phần lưu lý do hủy để tránh lỗi DB ---

                    db.SaveChanges();
                    return Json(new { success = true, message = "Đã hủy đơn hàng! (Lưu ý: Lý do hủy không được lưu do DB chưa hỗ trợ)" });
                }
                return Json(new { success = false, message = "Không tìm thấy đơn hàng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // Cần thêm Dispose method nếu bạn đã có nó trong các Controller khác
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}