using System.ComponentModel.DataAnnotations;
namespace QlyKhachHang.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập hoặc email là bắt buộc")]
        [StringLength(100)]
        [Display(Name = "Tên đăng nhập hoặc Email")]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Nhớ tôi")]
        public bool RememberMe { get; set; }
    }
}