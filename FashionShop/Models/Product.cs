using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Models
{
    public class Product
    {
        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(255, ErrorMessage = "Tên sản phẩm không được vượt quá 255 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả ngắn không được vượt quá 500 ký tự")]
        [Display(Name = "Mô tả ngắn")]
        public string MoTaNgan { get; set; }

        [AllowHtml]
        [Display(Name = "Mô tả chi tiết")]
        public string MoTaChiTiet { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        [Display(Name = "Giá gốc")]
        public decimal Gia { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi phải lớn hơn 0")]
        [Display(Name = "Giá khuyến mãi")]
        public decimal? GiaKM { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; } = true;

        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public int MaDanhMuc { get; set; }

        [Display(Name = "Hãng")]
        public int? MaHang { get; set; }

        [Display(Name = "Màu sắc")]
        public int? MaMau { get; set; }

        [Display(Name = "Kích thước")]
        public int? MaSize { get; set; }

        // Additional properties for display
        public string TenDanhMuc { get; set; }
        public string TenHang { get; set; }
        public string TenMau { get; set; }
        public string TenSize { get; set; }
    }
}