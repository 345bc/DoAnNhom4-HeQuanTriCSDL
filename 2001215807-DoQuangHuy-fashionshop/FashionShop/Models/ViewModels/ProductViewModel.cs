using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Product_Id { get; set; }
        public string ProductName { get; set; }
        public string ProductSlug { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public int Quantity { get; set; }
        public int Category_Id { get; set; }
        public int Brand_Id { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
    }
}