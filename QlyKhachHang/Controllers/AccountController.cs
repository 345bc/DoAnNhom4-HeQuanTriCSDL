using Microsoft.AspNetCore.Mvc;
using QlyKhachHang.Models;
using QlyKhachHang.Services;
namespace QlyKhachHang.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAuthenticationService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }
        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var (success, message) = await _authService.RegisterAsync(model);
            if (success)
            {
                TempData["Success"] = message + ". Vui lòng đăng nhập.";
                return RedirectToAction(nameof(Login));
            }
            else
            {
                ModelState.AddModelError("", message);
                return View(model);
            }
        }
        // GET: Account/Login
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var customer = await _authService.LoginAsync(model.UsernameOrEmail, model.Password);
            if (customer == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập/email hoặc mật khẩu không chính xác");
                return View(model);
            }
            // Set session immediately - don't wait for last login update
            HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
            HttpContext.Session.SetString("CustomerName", customer.CustomerName);
            HttpContext.Session.SetString("CustomerEmail", customer.Email);
            
            // Fire and forget - update last login in background (no await)
            _ = _authService.UpdateLastLoginAsync(customer.CustomerId);
            
            TempData["Success"] = $"Chào mừng {customer.CustomerName}!";
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            // GET Login - Nếu đã login → Vào Dashboard
            if (customerId != null)
                return RedirectToAction("Index", "CustomerDashboard");

            // POST Login thành công → Vào Dashboard (không phải Home)
            return RedirectToAction("Index", "CustomerDashboard");
        }
        // GET: Account/Profile
        public IActionResult Profile()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction(nameof(Login));
            }
            return View();
        }
        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Đăng xuất thành công";
            return RedirectToAction(nameof(Login));
        }
    }
}