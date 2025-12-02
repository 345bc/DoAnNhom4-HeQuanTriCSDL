using System;
using System.Linq;
using System.Web.Mvc;
using QlyDonHang_DoAn_hqtcsdl.Models;

namespace QlyDonHang_DoAn_hqtcsdl.Controllers
{
    public class CustomerController : Controller
    {
        // SỬA LỖI: Dùng FashionShopDb thay vì Entities để giống OrderController
        // Vì OrderController chạy được, nên FashionShopDb là class đúng
        private FashionShopDb db = new FashionShopDb();

        // GET: Customer
        public ActionResult Index()
        {
            try
            {
                var customers = db.KhachHangs.ToList();
                return View(customers);
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi ra View để dễ debug nếu có sự cố
                ViewBag.Error = "Lỗi kết nối: " + ex.Message;
                return View(new System.Collections.Generic.List<KhachHang>());
            }
        }

        // GET: Customer/History/5
        public ActionResult LichSu(int id)
        {
            try
            {
                // 1. Kiểm tra khách hàng có tồn tại không
                var customer = db.KhachHangs.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                // 2. Lấy danh sách đơn hàng của khách đó
                var orders = db.Orders.Where(o => o.MaKH == id)
                                      .OrderByDescending(o => o.OrderDate)
                                      .ToList();

                // 3. Truyền tên khách hàng sang View để hiển thị tiêu đề
                ViewBag.CustomerName = customer.TenKH;
                ViewBag.CustomerID = id;

                // Trả về View History.cshtml
                return View("LichSu", orders);
            }
            catch (Exception ex)
            {
                return Content("Lỗi: " + ex.Message);
            }
        }
    }
}