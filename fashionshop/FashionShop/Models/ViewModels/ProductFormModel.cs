using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Models.ViewModels
{
    public class ProductFormModel
    {
        public Product Form { get; set; }
        public List<Product> List { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> Colors { get; set; }
        public List<SelectListItem> Sizes { get; set; }

        public ProductFormModel()
        {
            Form = new Product();
            List = new List<Product>();
            Categories = new List<SelectListItem>();
            Brands = new List<SelectListItem>();
            Colors = new List<SelectListItem>();
            Sizes = new List<SelectListItem>();
        }
    }
}