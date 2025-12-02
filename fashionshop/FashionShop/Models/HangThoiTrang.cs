using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class HangThoiTrang
    {
        [Display(Name = "Mã hãng")]
        public int MaHang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên hãng")]
        [StringLength(150, ErrorMessage = "Tên hãng không được vượt quá 150 ký tự")]
        [Display(Name = "Tên hãng")]
        public string TenHang { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [StringLength(100, ErrorMessage = "Xuất xứ không được vượt quá 100 ký tự")]
        [Display(Name = "Xuất xứ")]
        public string XuatXu { get; set; }

        [StringLength(200, ErrorMessage = "Website không được vượt quá 200 ký tự")]
        [Url(ErrorMessage = "Định dạng URL không hợp lệ")]
        [Display(Name = "Website")]
        public string Website { get; set; }

        [StringLength(300, ErrorMessage = "Đường dẫn logo không được vượt quá 300 ký tự")]
        [Display(Name = "Logo")]
        public string Logo { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; } = true; // IsActive
    }
}