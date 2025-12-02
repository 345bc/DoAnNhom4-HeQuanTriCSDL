using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web;

namespace FashionShop.Models
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(200, ErrorMessage = "Tên danh mục không được vượt quá 200 ký tự")]
        [Display(Name = "Tên danh mục")]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Slug")]
        public string CategorySlug { get; set; }

        [StringLength(1000)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(500)]
        [Display(Name = "Hình ảnh")]
        public string CategoryImageUrl { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool IsActive { get; set; }

        [Display(Name = "Danh mục cha")]
        public int? ParentCategory_Id { get; set; }

        [Display(Name = "Hãng thời trang")]
        public int? Brand_Id { get; set; }

        [Display(Name = "Nổi bật")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Thứ tự sắp xếp")]
        public int SortOrder { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Người tạo")]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }

        // Navigation Properties
        [ForeignKey("Brand_Id")]
        public virtual HangThoiTrang Brand { get; set; }

        [ForeignKey("ParentCategory_Id")]
        public virtual Category ParentCategory { get; set; }

        // Collection navigation properties
        public virtual ICollection<Category> SubCategories { get; set; }

        // Property không map vào database - dùng cho upload file
        [NotMapped]
        [Display(Name = "Chọn hình ảnh")]
        public HttpPostedFileBase ImageFile { get; set; }

        // Constructor
        public Category()
        {
            IsActive = true;
            IsFeatured = false;
            SortOrder = 0;
            CreatedDate = DateTime.Now;
            SubCategories = new HashSet<Category>();
        }
    }
}