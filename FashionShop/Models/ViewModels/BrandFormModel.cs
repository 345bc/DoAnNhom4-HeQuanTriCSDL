using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.ViewModels
{
    public class BrandFormModel
    {
        public HangThoiTrang Form { get; set; }

        public List<HangThoiTrang> List { get; set; }

        public BrandFormModel()
        {
            Form = new HangThoiTrang();
            List = new List<HangThoiTrang>();
        }
    }
}