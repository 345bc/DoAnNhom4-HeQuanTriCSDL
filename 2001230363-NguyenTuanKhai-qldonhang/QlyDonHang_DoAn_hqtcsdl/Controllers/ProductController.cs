using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity; // Cần thiết để dùng .Include()
using QlyDonHang_DoAn_hqtcsdl.Models;
using System.Collections.Generic;

namespace QlyDonHang_DoAn_hqtcsdl.Controllers
{
    public class ProductController : Controller
    {
        private FashionShopDb db = new FashionShopDb();

        // GET: Product
        public ActionResult Index()
        {
            try
            {
                // Kiểm tra kết nối DB trước
                if (!db.Database.Exists())
                {
                    throw new Exception("Không kết nối được Cơ sở dữ liệu. Hãy kiểm tra Connection String.");
                }

                // Lấy danh sách sản phẩm
                // Sắp xếp giảm dần theo ID để thấy sản phẩm mới thêm
                var products = db.Products
                                 .OrderByDescending(p => p.Product_Id)
                                 .ToList();

                // Nếu count = 0 thì có thể do chưa có data hoặc query sai
                if (products.Count == 0)
                {
                    ViewBag.Warning = "Kết nối thành công nhưng bảng Product đang trống.";
                }

                return View(products);
            }
            catch (Exception ex)
            {
                // Lấy chi tiết lỗi (bao gồm cả InnerException nếu có)
                string errorMsg = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMsg += " -> Chi tiết: " + ex.InnerException.Message;
                }

                ViewBag.Error = errorMsg;

                // Trả về danh sách rỗng để View vẫn chạy được khung
                return View(new List<Product>());
            }
        }

        // Các Action Create, Edit, Delete... (giữ nguyên hoặc thêm sau)
    }
}