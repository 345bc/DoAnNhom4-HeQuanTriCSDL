using ESHOPPER.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESHOPPER.Controllers.Admin
{
    [RoutePrefix("Admin/Customer")]

    public class CustomerController : Controller
    {
        private FashionShopDbEntities db = new FashionShopDbEntities();

        // GET: Customer
        [Route("")]

        public ActionResult Index()
        {
            var khachHangs = db.KhachHangs.Include(k => k.User);
            return View(khachHangs.ToList());
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
