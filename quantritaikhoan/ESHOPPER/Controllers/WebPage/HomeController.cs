using ESHOPPER.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESHOPPER.Controllers.WebPage
{

    public class HomeController : Controller
    {
        FashionShopDbEntities db = new FashionShopDbEntities();
        [Route("")]
        public ActionResult Index()
        {

            //var vm = new HomeViewModel
            //{
            //    Discounts = db.Discounts.Where(d => d.IsActive == true)
            //                            .OrderByDescending(d => d.StartDate)
            //                            .ToList(),
            //    DanhMucSanPhams = db.DanhMucSanPhams.ToList(),
            //    nhaCungCaps = db.NhaCungCaps.ToList(),
            //    SanPhams = db.SanPhams
            //            .OrderByDescending(p => p.MaSP)
            //            .Take(8) // lấy 8 sản phẩm mới nhất
            //            .ToList(),
            //    SanPhamNgauNhiens = db.SanPhams
            //                    .OrderBy(r => Guid.NewGuid()) // sắp xếp ngẫu nhiên
            //                    .Take(8) // số lượng sản phẩm muốn hiển thị
            //                    .ToList()
            //};


            //return View(vm);
            return View();
        }

        [Route("Infomation")]
        public ActionResult Infomation()
        {
            //1.KIỂM TRA ĐĂNG NHẬP
            //Nếu chưa có Session UserID -> Đá về trang Login
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            int userId = (int)Session["UserID"];

            // 2. LẤY THÔNG TIN KHÁCH HÀNG
            // Dùng .Include("User") để lấy kèm thông tin Email từ bảng Users
            // Logic: Tìm dòng trong bảng KhachHang có UserId trùng với người đang đăng nhập
            var khachHang = db.KhachHangs.Include("User").FirstOrDefault(k => k.UserId == userId);

            // Trường hợp: Tài khoản User tồn tại nhưng chưa có dữ liệu bên bảng KhachHang
            if (khachHang == null)
            {
                // Có thể Redirect về trang chủ hoặc trang báo lỗi tùy bạn
                return RedirectToAction("Index", "Home");
            }

            // 3. LẤY LỊCH SỬ ĐƠN HÀNG
            // Lọc theo MaKH của khách hàng vừa tìm được
            // Sắp xếp giảm dần theo ngày đặt (đơn mới nhất lên đầu)
            //var orderHistory = db.Orders
            //                     .Where(o => o.MaKH == khachHang.MaKH)
            //                     .OrderByDescending(o => o.OrderDate)
            //                     .ToList();

            // 4. TRUYỀN DỮ LIỆU SANG VIEW
            // Dữ liệu phụ (List đơn hàng) -> Bỏ vào ViewBag
            //ViewBag.OrderHistory = orderHistory;

            // Dữ liệu chính (Thông tin cá nhân) -> Truyền làm Model
            return View(khachHang);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // Chống giả mạo request
        public ActionResult UpdateInfo(KhachHang formModel)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                var existingCustomer = db.KhachHangs.Find(formModel.MaKH);

                if (existingCustomer != null)
                {
                    int currentUserId = (int)Session["UserID"];
                    if (existingCustomer.UserId != currentUserId)
                    {
                        TempData["Error"] = "Bạn không có quyền sửa thông tin này!";
                        return RedirectToAction("Index");
                    }

                    existingCustomer.TenKH = formModel.TenKH;
                    existingCustomer.SoDT = formModel.SoDT;
                    existingCustomer.BirthDayKH = formModel.BirthDayKH;
                    existingCustomer.GioiTinhKH = formModel.GioiTinhKH;

                    db.SaveChanges();

                    TempData["Success"] = "Cập nhật hồ sơ thành công!";
                }
                else
                {
                    TempData["Error"] = "Không tìm thấy dữ liệu khách hàng.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi hệ thống: " + ex.Message;
            }

            // Quay lại trang Profile
            return RedirectToAction("Infomation");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            // 1. Kiểm tra session đăng nhập
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // 2. Validate dữ liệu đầu vào
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                TempData["Error"] = "Vui lòng nhập đầy đủ các trường mật khẩu.";
                return RedirectToAction("Index");
            }

            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "Mật khẩu xác nhận không khớp.";
                return RedirectToAction("Index");
            }

            if (newPassword.Length < 6)
            {
                TempData["Error"] = "Mật khẩu mới phải có ít nhất 6 ký tự.";
                return RedirectToAction("Index");
            }

            try
            {
                int userId = (int)Session["UserID"];

                // 3. Lấy thông tin User hiện tại từ DB để kiểm tra mật khẩu cũ
                var user = db.Users.Find(userId);

                if (user != null)
                {
                    // --- BƯỚC QUAN TRỌNG: KIỂM TRA MẬT KHẨU CŨ ---
                    bool isOldPasswordCorrect = false;

                    // Kiểm tra xem pass trong DB là Hash hay Plain text (Hỗ trợ dữ liệu cũ)
                    if (user.Password.StartsWith("$2a$") || user.Password.StartsWith("$2b$") || user.Password.StartsWith("$2y$"))
                    {
                        // Nếu là Hash chuẩn -> Dùng Verify
                        isOldPasswordCorrect = BCrypt.Net.BCrypt.Verify(currentPassword, user.Password);
                    }
                    else
                    {
                        // Nếu là pass thường (chưa hash) -> So sánh chuỗi
                        isOldPasswordCorrect = (currentPassword == user.Password);
                    }

                    if (!isOldPasswordCorrect)
                    {
                        TempData["Error"] = "Mật khẩu hiện tại không chính xác.";
                        return RedirectToAction("Index");
                    }

                    // --- BƯỚC CẬP NHẬT: GỌI STORED PROCEDURE ---

                    // 4. Băm mật khẩu mới trước khi gửi xuống DB
                    string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                    // 5. Chuẩn bị tham số SQL
                    var pUserId = new SqlParameter("@UserID", userId);
                    var pHash = new SqlParameter("@NewPasswordHash", newPasswordHash);

                    // Tham số Output để hứng kết quả
                    var pResult = new SqlParameter("@Result", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };

                    // 6. Thực thi lệnh
                    db.Database.ExecuteSqlCommand(
                        "EXEC sp_ChangeUserPassword @UserID, @NewPasswordHash, @Result OUT",
                        pUserId, pHash, pResult
                    );

                    // 7. Kiểm tra kết quả từ SQL trả về
                    int result = (int)pResult.Value;

                    if (result == 0)
                    {
                        TempData["Success"] = "Đổi mật khẩu thành công!";
                    }
                    else
                    {
                        TempData["Error"] = "Lỗi hệ thống: Không thể cập nhật mật khẩu.";
                    }
                }
                else
                {
                    TempData["Error"] = "Tài khoản không tồn tại hoặc đã bị xóa.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi Exception: " + ex.Message;
            }

            // Quay lại trang Profile
            return RedirectToAction("Infomation");
        }
        //[ChildActionOnly] // Đảm bảo action này chỉ được gọi từ bên trong 1 View
        //public ActionResult CategoryMenu()
        //{
        //    // 1. Lấy DANH SÁCH danh mục từ CSDL
        //    var model = db.DanhMucSanPhams.ToList();

        //    // 2. Gửi DANH SÁCH này đến PartialView
        //    return PartialView("ParCategories", model);
        //}
        //[Route("About")]

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}
        //[Route("Contact")]

        //public ActionResult Contact()
        //{
        //    var model = new HomeViewModel
        //    {
        //        DanhMucSanPhams = db.DanhMucSanPhams.ToList()
        //    };
        //    return View(model);
        //}

        //[Route("Shop")]
        //public ActionResult Shop(string maDM) // 👈 Đổi từ int? danhMucID sang string maDM
        //{
        //    var sanPhamsQuery = db.SanPhams
        //        .AsQueryable();

        //    // Kiểm tra xem maDM có giá trị không
        //    if (!string.IsNullOrEmpty(maDM))
        //    {
        //        // Lọc thẳng bằng string, không cần chuyển đổi
        //        sanPhamsQuery = sanPhamsQuery.Where(p => p.MaDM == maDM);
        //    }

        //    var model = new HomeViewModel
        //    {
        //        DanhMucSanPhams = db.DanhMucSanPhams.ToList(), // Tải danh mục cho view này (nếu cần)
        //        SanPhams = sanPhamsQuery
        //        .Take(9)
        //        //.OrderByDescending(p => p.MaSP)
        //        .ToList()
        //    };

        //    // ❗️ Gửi maDM (kiểu string) về View
        //    ViewBag.SelectedCategoryID = maDM;

        //    return View(model);
        //}
        ////[Route("Details")]
        ////public ActionResult ProductDetails()
        ////{
        ////    return View();
        ////}
        //[Route("ProductDetails")]
        //public ActionResult ProductDetails(string id)
        //{
        //    // 1. Vẫn tìm sản phẩm và 'Include' biến thể như cũ
        //    var sanPham = db.SanPhams
        //                    .Include(p => p.BienTheSanPhams)
        //                    .FirstOrDefault(p => p.MaSP == id);

        //    if (sanPham == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // 2. Lọc ra các list size/màu duy nhất (như đã làm)
        //    var uniqueSizes = sanPham.BienTheSanPhams
        //                        .Where(b => !string.IsNullOrEmpty(b.MaSize))
        //                        .Select(b => b.MaSize)
        //                        .Distinct()
        //                        .ToList();

        //    var uniqueColors = sanPham.BienTheSanPhams
        //                        .Where(b => !string.IsNullOrEmpty(b.MaMau))
        //                        .Select(b => b.MaMau)
        //                        .Distinct()
        //                        .ToList();

        //    // 3. ĐÂY LÀ BƯỚC QUAN TRỌNG NHẤT
        //    // Tạo một đối tượng ProductDetailViewModel mới
        //    var viewModel = new ProductDetailsViewModel
        //    {
        //        SanPhamChinh = sanPham,         // Gán sản phẩm vào
        //        CacSizeDuyNhat = uniqueSizes,   // Gán list size vào
        //        CacMauDuyNhat = uniqueColors,    // Gán list màu vào
        //        SanPhamNgauNhiens = db.SanPhams
        //                        .OrderBy(r => Guid.NewGuid()) // sắp xếp ngẫu nhiên
        //                        .Take(8) // số lượng sản phẩm muốn hiển thị
        //                        .ToList()
        //    };

        //    // 4. Trả về 'viewModel' (thay vì 'sanPham')
        //    return View(viewModel);
        //}
        //[Route("Cart/Add")]
        //[HttpPost]
        //public ActionResult AddToCart(string productId, string selectedSize, string selectedColor, int quantity)
        //{
        //    string size = string.IsNullOrEmpty(selectedSize) ? null : selectedSize;
        //    string color = string.IsNullOrEmpty(selectedColor) ? null : selectedColor;

        //    // Tìm biến thể MẶC ĐỊNH (MaSize=NULL, MaMau=NULL)
        //    var matchedVariant = db.BienTheSanPhams.FirstOrDefault(b =>
        //        b.MaSP == productId &&
        //        b.MaSize == size &&
        //        b.MaMau == color
        //    );
        //    // 1. Dùng thông tin nhận được để tìm MaBienThe chính xác
        //    //var matchedVariant = db.BienTheSanPhams.FirstOrDefault(b =>
        //    //    b.MaSP == productId &&
        //    //    b.MaSize == selectedSize &&
        //    //    b.MaMau == selectedColor
        //    //);

        //    // 2. Kiểm tra xem biến thể có tồn tại không
        //    if (matchedVariant == null)
        //    {
        //        TempData["ErrorMessage"] = "Sản phẩm với tùy chọn này không tồn tại.";
        //        return RedirectToAction("ProductDetails", "Home", new { id = productId });
        //    }

        //    // 3. Kiểm tra tồn kho
        //    if (matchedVariant.SoLuongTon < quantity)
        //    {
        //        TempData["ErrorMessage"] = "Số lượng tồn kho không đủ.";
        //        return RedirectToAction("ProductDetails", "Home", new { id = productId });
        //    }

        //    // --- SỬA LỖI LƯU TRỮ SESSION ---
        //    // 4. Lấy giỏ hàng từ Session (sử dụng List<CartItem> an toàn)
        //    var cart = Session["Cart"] as List<CartItem>;
        //    if (cart == null)
        //    {
        //        cart = new List<CartItem>();
        //    }

        //    // 5. Kiểm tra xem sản phẩm đã có trong giỏ chưa
        //    // Truy cập MaBienThe trực tiếp trên CartItem
        //    var existingItem = cart.FirstOrDefault(item => item.MaBienThe == matchedVariant.MaBienThe);

        //    if (existingItem != null)
        //    {
        //        // Nếu đã có -> Cập nhật số lượng
        //        existingItem.SoLuong += quantity;
        //    }
        //    else
        //    {
        //        // Nếu chưa có -> Thêm mới (SỬA LỖI CÚ PHÁP)
        //        cart.Add(new CartItem
        //        {
        //            MaBienThe = matchedVariant.MaBienThe, // Thuộc tính của CartItem
        //            SoLuong = quantity,
        //            Gia = matchedVariant.GiaBan ?? 0
        //        });
        //    }

        //    // 6. Lưu giỏ hàng lại vào Session
        //    Session["Cart"] = cart;

        //    // 7. Chuyển hướng người dùng đến trang Giỏ hàng
        //    return RedirectToAction("Cart");
        //}


        // --- 2. ACTION HIỂN THỊ TRANG GIỎ HÀNG ---
        // (Dùng code giống ShoppingCartController.Index)
        // --- ACTION HIỂN THỊ TRANG GIỎ HÀNG (CẬP NHẬT) ---
        // Action Cart() SỬA ĐÚNG (Dùng CartItem từ Session)
        //[Route("Cart")]

        //public ActionResult Cart()
        //{
        //    var cart = Session["Cart"] as List<CartItem>; // <--- Dùng CartItem từ Session
        //    var viewModel = new List<CartViewModel>();
        //    decimal subtotal = 0;


        //    if (cart != null)
        //    {
        //        foreach (var item in cart) // item là CartItem
        //        {
        //            var bienThe = db.BienTheSanPhams
        //                            .Include(b => b.SanPham)
        //                            .FirstOrDefault(b => b.MaBienThe == item.MaBienThe); // <--- SỬA: Dùng item.MaBienThe


        //            if (bienThe != null)
        //            {
        //                var cartItemVM = new CartViewModel // <--- Dùng CartViewModel để hiển thị
        //                {
        //                    // Lấy dữ liệu chi tiết từ CSDL
        //                    TenSP = bienThe.SanPham.TenSanPham,
        //                    AnhSP = bienThe.SanPham.AnhSP,
        //                    MaSize = bienThe.MaSize,
        //                    MaMau = bienThe.MaMau,

        //                    // Lấy dữ liệu giỏ hàng từ Session
        //                    MaBienThe = item.MaBienThe, // MaBienThe
        //                    SoLuong = item.SoLuong,
        //                    Gia = item.Gia,             // Giá đã chốt

        //                    // Gán BienTheSP để tính toán
        //                };

        //                subtotal += cartItemVM.ThanhTien;
        //                viewModel.Add(cartItemVM);
        //            }
        //        }
        //    }
        //    // ... (logic ViewBag còn lại) ...
        //    // Trong Action Cart():
        //    decimal shipping = (subtotal > 0 && subtotal < 500000) ? 30000 : 0;
        //    ViewBag.Subtotal = subtotal; // Luôn là số 0 hoặc lớn hơn
        //    ViewBag.Shipping = shipping; // Luôn là số 0 hoặc 30000
        //    ViewBag.Total = subtotal + shipping; // Luôn là số
        //    return View(viewModel);
        //}


        //// --- 3. ACTION XÓA KHỎI GIỎ HÀNG ---
        //[Route("Cart/Remove/{id}")]
        //public ActionResult RemoveFromCart(string id) // 'id' là MaBienThe
        //{
        //    var cart = Session["Cart"] as List<CartItem>; // <--- SỬA: Dùng CartItem
        //    if (cart != null)
        //    {
        //        // SỬA: Truy cập item.MaBienThe
        //        var itemToRemove = cart.FirstOrDefault(item => item.MaBienThe == id);
        //        if (itemToRemove != null)
        //        {
        //            cart.Remove(itemToRemove);
        //        }
        //        Session["Cart"] = cart;
        //    }
        //    return RedirectToAction("Cart");
        //}


        //// --- 4. ACTION CẬP NHẬT SỐ LƯỢNG ---
        //[Route("Cart/Update")]
        //[HttpPost]
        //public ActionResult UpdateCart(string id, int quantity) // 'id' là MaBienThe
        //{
        //    var cart = Session["Cart"] as List<CartItem>; // <--- SỬA: Dùng CartItem
        //    if (cart != null)
        //    {
        //        // SỬA: Truy cập item.MaBienThe
        //        var itemToUpdate = cart.FirstOrDefault(item => item.MaBienThe == id);

        //        if (itemToUpdate != null)
        //        {
        //            if (quantity > 0)
        //            {
        //                itemToUpdate.SoLuong = quantity;
        //            }
        //            else
        //            {
        //                cart.Remove(itemToUpdate);
        //            }
        //        }
        //        Session["Cart"] = cart;
        //    }
        //    return RedirectToAction("Cart");
        //}

    }
}