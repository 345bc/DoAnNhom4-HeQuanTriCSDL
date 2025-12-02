using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class Brand
    {
        public int Brand_Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Email { get; set; }
    }
}