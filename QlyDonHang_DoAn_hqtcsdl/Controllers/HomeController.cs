using System;
using System.Linq;
using System.Web.Mvc;
using QlyDonHang_DoAn_hqtcsdl.Models;
using System.Collections.Generic;

namespace QlyDonHang_DoAn_hqtcsdl.Controllers
{
    public class HomeController : Controller
    {
        private FashionShopDb db = new FashionShopDb();

        public ActionResult Index()
        {
            try
            {
                // Lấy tháng và năm hiện tại
                int currentMonth = DateTime.Now.Month;
                int currentYear = DateTime.Now.Year;

                // 1. Doanh thu tháng này (Chỉ tính đơn HOÀN THÀNH trong tháng hiện tại)
                // Lưu ý: Cần kiểm tra kỹ chuỗi trạng thái trong DB là "Hoàn thành", "Completed", hay "Success"...
                // Ở đây tôi dùng .Contains("Hoàn thành") để linh hoạt hơn
                var monthlyRevenue = db.Orders
                    .Where(o => o.Status.Contains("Hoàn thành") &&
                                o.OrderDate.Month == currentMonth &&
                                o.OrderDate.Year == currentYear)
                    .Sum(o => (decimal?)o.TotalAmount) ?? 0;

                ViewBag.MonthlyRevenue = monthlyRevenue;
                ViewBag.CurrentMonth = currentMonth; // Truyền tháng để hiển thị tiêu đề

                // 2. Tổng số đơn hàng (Tất cả trạng thái)
                ViewBag.TotalOrders = db.Orders.Count();

                // 3. Số lượng khách hàng
                ViewBag.TotalCustomers = db.KhachHangs.Count();

                // 4. Tổng sản phẩm tồn kho
                ViewBag.TotalProducts = db.Products.Sum(p => (int?)p.Quantity) ?? 0;

                // 5. Lấy 5 đơn hàng mới nhất
                var recentOrders = db.Orders
                    .Include("KhachHang") // Eager loading để tránh lỗi null khi view gọi item.KhachHang.TenKH
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .ToList();

                return View(recentOrders);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể kết nối đến máy chủ Database: " + ex.Message;
                return View(new List<Order>());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}