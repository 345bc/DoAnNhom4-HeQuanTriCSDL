using BCrypt.Net;
using ESHOPPER.Helpers; // Giả định chứa JwtHelper và JwtConfig
using ESHOPPER.Models;
using System;
using System.Data.Entity.Core.Objects; // Cần thiết cho tham số OUTPUT
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ESHOPPER.Controllers.Auth
{
    [RoutePrefix("Auth")]
    public class AuthController : Controller
    {
        private FashionShopDbEntities db = new FashionShopDbEntities();

        // ----------------------------------------------------
        // 1. ĐĂNG NHẬP (sp_LoginUser)
        // ----------------------------------------------------

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public JsonResult Login(string loginInput, string password)
        {
            try
            {
                var resultParam = new ObjectParameter("Result", typeof(int));
                var userIdParam = new ObjectParameter("UserID", typeof(int));
                var nameParam = new ObjectParameter("Name", typeof(string));
                var roleParam = new ObjectParameter("Role", typeof(string));
                var passwordParam = new ObjectParameter("Password", typeof(string));

                db.sp_LoginUser(
                    loginInput,
                    resultParam,
                    userIdParam,
                    nameParam,
                    roleParam,
                    passwordParam
                );

                int result = (int)resultParam.Value;

                if (result == 1)
                {
                    return Json(new { success = false, message = "Email hoặc mật khẩu không đúng!" });
                }

                string storedPassword = passwordParam.Value.ToString();

                if (!BCrypt.Net.BCrypt.Verify(password, storedPassword))
                {
                    return Json(new { success = false, message = "Email hoặc mật khẩu không đúng!" });
                }

                Session["UserID"] = (int)userIdParam.Value;
                Session["UserName"] = nameParam.Value.ToString();
                Session["Role"] = roleParam.Value.ToString();

                string role = roleParam.Value.ToString();

                var authTicket = new FormsAuthenticationTicket(
                    1,
                    loginInput,                  
                    DateTime.Now,
                    DateTime.Now.AddHours(2),     
                    false,                        
                    role                          
                );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                string userName = nameParam.Value.ToString();
                string userRole = roleParam.Value.ToString();
                var token = JwtHelper.GenerateToken(loginInput, userRole, userName);

                return Json(new
                {
                    success = true,
                    message = "Đăng nhập thành công!",
                    token = token,
                    role = userRole,
                    userName = userName
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống khi đăng nhập: " + ex.Message });
            }
        }


        //[HttpPost]
        //[Route("login")]
        //public JsonResult Login(string loginInput, string password)
        //{
        //    try
        //    {
        //        // 1. Khai báo tham số OUTPUT
        //        var resultParam = new ObjectParameter("Result", typeof(int));
        //        var userIdParam = new ObjectParameter("UserID", typeof(int));
        //        var nameParam = new ObjectParameter("Name", typeof(string));
        //        var roleParam = new ObjectParameter("Role", typeof(string));
        //        var passwordParam = new ObjectParameter("Password", typeof(string));

        //        // 2. Gọi SP. Lưu ý: SP chỉ tìm kiếm theo Email (@Email).
        //        // Nếu loginInput là username, bạn cần một SP khác hoặc logic tìm kiếm bổ sung.
        //        // Ở đây, ta dùng loginInput làm Email.
        //        db.sp_LoginUser(
        //            loginInput,
        //            resultParam,
        //            userIdParam,
        //            nameParam,
        //            roleParam,
        //            passwordParam
        //        );

        //        int result = (int)resultParam.Value;

        //        if (result == 1) // 1: Không tìm thấy
        //        {
        //            return Json(new { success = false, message = "Email hoặc mật khẩu không đúng!" });
        //        }

        //        // Lấy Hash đã lưu
        //        string storedPassword = passwordParam.Value.ToString();

        //        // 3. Verify mật khẩu bằng BCrypt
        //        if (!BCrypt.Net.BCrypt.Verify(password, storedPassword))
        //        {
        //            return Json(new { success = false, message = "Email hoặc mật khẩu không đúng!" });
        //        }

        //        Session["UserID"] = (int)userIdParam.Value;   // <-- QUAN TRỌNG NHẤT: HomeController cần cái này
        //        Session["UserName"] = nameParam.Value.ToString();
        //        Session["Role"] = roleParam.Value.ToString();

        //        // 4. Đăng nhập thành công, tạo JWT
        //        string userName = nameParam.Value.ToString();
        //        string userRole = roleParam.Value.ToString();
        //        var token = JwtHelper.GenerateToken(loginInput, userRole, userName);

        //        return Json(new
        //        {
        //            success = true,
        //            message = "Đăng nhập thành công!",
        //            token = token,
        //            role = userRole,
        //            userName = userName
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Ghi log lỗi
        //        return Json(new { success = false, message = "Lỗi hệ thống khi đăng nhập: " + ex.Message });
        //    }
        //}


        // ----------------------------------------------------
        // 2. ĐĂNG KÝ (sp_RegisterUserWithSeparateName)
        // ----------------------------------------------------

        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public JsonResult Register(string name, string username, string email, string password, string phone, DateTime? birthDate, string gender)
        {
            try
            {
                // 1. Hash mật khẩu bằng BCrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                // 2. Khai báo tham số OUTPUT
                var resultParam = new ObjectParameter("Result", typeof(int));
                var userIdParam = new ObjectParameter("UserID", typeof(int));
                var maKHParam = new ObjectParameter("MaKH", typeof(int));

                // 3. Gọi Stored Procedure
                db.sp_RegisterUserWithSeparateName(
                    name,           // @CustomerName (Tên đầy đủ)
                    username,       // @UserName (Tên đăng nhập)
                    email,
                    hashedPassword,
                    "Customer",     // @Role (mặc định)
                    phone,
                    birthDate,
                    gender,
                    resultParam,
                    userIdParam,
                    maKHParam
                );

                int result = (int)resultParam.Value;

                switch (result)
                {
                    case 0:
                        return Json(new { success = true, message = "Đăng ký thành công! Bạn có thể đăng nhập." });
                    case 1:
                        return Json(new { success = false, message = "Email hoặc Tên đăng nhập đã tồn tại!" });
                    case -1:
                    default:
                        return Json(new { success = false, message = "Lỗi hệ thống khi đăng ký. Vui lòng thử lại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                return Json(new { success = false, message = "Lỗi kết nối database: " + ex.Message });
            }
        }
        [HttpGet]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Vui lòng nhập Email.";
                return View();
            }

            try
            {
                // 1. Sinh mật khẩu ngẫu nhiên (6 ký tự) để người dùng nhìn thấy
                string plainTextPassword = GenerateRandomPassword(6);

                // 2. Băm mật khẩu này để lưu vào Database (Bảo mật)
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);

                // 3. Chuẩn bị tham số gọi SP
                var pEmail = new SqlParameter("@TargetEmail", email);

                // QUAN TRỌNG: Gửi mật khẩu ĐÃ BĂM xuống SQL
                var pPass = new SqlParameter("@NewPassword", hashedPassword);

                var pAdmin = new SqlParameter("@AdminName", "SYSTEM (Auto Reset)");
                var pResult = new SqlParameter("@Result", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };

                // 4. Gọi SP để lưu hash vào DB
                db.Database.ExecuteSqlCommand("EXEC sp_ResetUserPassword @TargetEmail, @NewPassword, @AdminName, @Result OUT",
                    pEmail, pPass, pAdmin, pResult);

                int result = (int)pResult.Value;

                if (result == 0)
                {
                    // 5. Thành công: Hiển thị mật khẩu GỐC (chưa băm) cho người dùng xem để họ đăng nhập
                    ViewBag.Success = "Thành công! Mật khẩu mới là: " + plainTextPassword;
                    ViewBag.ShowReturnLogin = true;
                }
                else
                {
                    ViewBag.Error = "Email không tồn tại trong hệ thống.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi hệ thống: " + ex.Message;
            }

            return View();
        }

        // --- HÀM PHỤ: SINH MẬT KHẨU NGẪU NHIÊN ---
        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        // ... Logout action ...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear(); // Xóa hết session
            Session.Abandon(); // Hủy phiên làm việc
            return Json(new { success = true });
        }
    }
}

//using BCrypt.Net;
//using ESHOPPER.Helpers; // Chứa JwtHelper
//using ESHOPPER.Models;
//using System;
//using System.Data.Entity.Core.Objects;
//using System.Web.Mvc;
//using System.Web.Security; // Cần thư viện này để xử lý Cookie

//namespace ESHOPPER.Controllers.Auth
//{
//    [RoutePrefix("Auth")]
//    public class AuthController : Controller
//    {
//        private FashionShopDbEntities db = new FashionShopDbEntities();

//        // --- TRANG ĐĂNG NHẬP ---
//        [HttpGet]
//        [Route("login")]
//        [AllowAnonymous] // Cho phép người lạ truy cập
//        public ActionResult Login(string returnUrl)
//        {
//            // Nếu đã đăng nhập rồi thì đá về trang chủ luôn
//            if (User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("Index", "Home");
//            }

//            // Lưu lại trang người dùng muốn vào trước đó (nếu có)
//            ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }

//        // --- XỬ LÝ ĐĂNG NHẬP (AJAX) ---
//        [HttpPost]
//        [Route("login")]
//        public JsonResult Login(string loginInput, string password)
//        {
//            try
//            {
//                // 1. Khai báo tham số OUTPUT cho Stored Procedure
//                var resultParam = new ObjectParameter("Result", typeof(int));
//                var userIdParam = new ObjectParameter("UserID", typeof(int));
//                var nameParam = new ObjectParameter("Name", typeof(string));
//                var roleParam = new ObjectParameter("Role", typeof(string));
//                var passwordParam = new ObjectParameter("Password", typeof(string));

//                // 2. Gọi SP kiểm tra
//                db.sp_LoginUser(loginInput, resultParam, userIdParam, nameParam, roleParam, passwordParam);

//                int result = (int)resultParam.Value;

//                // Kiểm tra kết quả từ DB (1: Không tìm thấy user)
//                if (result == 1 || passwordParam.Value == null)
//                {
//                    return Json(new { success = false, message = "Tài khoản không tồn tại!" });
//                }

//                string storedPassword = passwordParam.Value.ToString();

//                // 3. So khớp mật khẩu Hash
//                if (!BCrypt.Net.BCrypt.Verify(password, storedPassword))
//                {
//                    return Json(new { success = false, message = "Mật khẩu không đúng!" });
//                }

//                // --- BƯỚC QUAN TRỌNG NHẤT: GHI COOKIE ---
//                // Đây là lệnh giúp [Authorize] hoạt động
//                string userName = nameParam.Value.ToString();
//                FormsAuthentication.SetAuthCookie(loginInput, false); // False: Không nhớ đăng nhập (tắt trình duyệt là mất)

//                // (Tùy chọn) Nếu bạn vẫn cần JWT cho việc khác thì giữ lại
//                string userRole = roleParam.Value.ToString();
//                // var token = JwtHelper.GenerateToken(loginInput, userRole, userName);

//                // 4. Trả về kết quả thành công cho Client
//                return Json(new
//                {
//                    success = true,
//                    message = "Đăng nhập thành công!",
//                    userName = userName,
//                    // Server chỉ định đường dẫn chuyển hướng
//                    redirectUrl = Url.Action("Index", "Home")
//                });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
//            }
//        }
//         //----------------------------------------------------
//                // 2. ĐĂNG KÝ (sp_RegisterUserWithSeparateName)
//                // ----------------------------------------------------

//                [HttpGet]
//        [Route("register")]
//        public ActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        [Route("register")]
//        public JsonResult Register(string name, string username, string email, string password, string phone, DateTime? birthDate, string gender)
//        {
//            try
//            {
//                // 1. Hash mật khẩu bằng BCrypt
//                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

//                // 2. Khai báo tham số OUTPUT
//                var resultParam = new ObjectParameter("Result", typeof(int));
//                var userIdParam = new ObjectParameter("UserID", typeof(int));
//                var maKHParam = new ObjectParameter("MaKH", typeof(int));

//                // 3. Gọi Stored Procedure
//                db.sp_RegisterUserWithSeparateName(
//                    name,           // @CustomerName (Tên đầy đủ)
//                    username,       // @UserName (Tên đăng nhập)
//                    email,
//                    hashedPassword,
//                    "Customer",     // @Role (mặc định)
//                    phone,
//                    birthDate,
//                    gender,
//                    resultParam,
//                    userIdParam,
//                    maKHParam
//                );

//                int result = (int)resultParam.Value;

//                switch (result)
//                {
//                    case 0:
//                        return Json(new { success = true, message = "Đăng ký thành công! Bạn có thể đăng nhập." });
//                    case 1:
//                        return Json(new { success = false, message = "Email hoặc Tên đăng nhập đã tồn tại!" });
//                    case -1:
//                    default:
//                        return Json(new { success = false, message = "Lỗi hệ thống khi đăng ký. Vui lòng thử lại." });
//                }
//            }
//            catch (Exception ex)
//            {
//                // Ghi log lỗi
//                return Json(new { success = false, message = "Lỗi kết nối database: " + ex.Message });
//            }
//        }

//        // --- ĐĂNG XUẤT ---
//        [HttpPost]
//        [Route("logout")]
//        public ActionResult Logout()
//        {
//            // Xóa Cookie xác thực
//            FormsAuthentication.SignOut();
//            Session.Abandon(); // Xóa sạch session nếu có dùng

//            return Json(new { success = true, redirectUrl = Url.Action("Login", "Auth") });
//        }
//    }
//}?