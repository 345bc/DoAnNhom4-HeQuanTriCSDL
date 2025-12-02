using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.ViewModels
{
    public class CategoryFormModel
    {
        public Category Form { get; set; }

        // Cho danh sách
        public List<Category> List { get; set; }

        // Dropdown data
        public List<HangThoiTrang> Brands { get; set; }

        // Phân trang
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        // Filter/Search
        public string SearchKeyword { get; set; }
        public int? BrandFilter { get; set; }
        public bool? IsActiveFilter { get; set; }

        public CategoryFormModel()
        {
            Form = new Category();
            List = new List<Category>();
            Brands = new List<HangThoiTrang>();
            CurrentPage = 1;
            PageSize = 10;
        }
    }
}