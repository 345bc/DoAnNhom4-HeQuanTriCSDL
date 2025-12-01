    //using System.Web.Mvc;
    //using System.Web.Routing;

    //namespace ESHOPPER
    //{
    //    public class RouteConfig
    //    {
    //        public static void RegisterRoutes(RouteCollection routes)
    //        {
    //            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

    //            // 1. Dòng này BẬT [RoutePrefix] (phải ở trên cùng)
    //            routes.MapMvcAttributeRoutes();
    //            routes.MapRoute(
    //    name: "Admin",
    //    url: "Admin/{controller}/{action}/{id}",
    //    defaults: new { action = "Index", id = UrlParameter.Optional },
    //    namespaces: new[] { "ESHOPPER.Controllers.Admin" }
    //);

    //            // 2. Route "Default" (cho trang Home/WebPage)
    //            routes.MapRoute(
    //                name: "Default",
    //                url: "{controller}/{action}/{id}",
    //                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },

    //                // ▼▼▼ DÒNG NÀY LÀ QUAN TRỌNG NHẤT ▼▼▼
    //                // Nó bảo route này "CHỈ TÌM" controller trong thư mục WebPage
    //                namespaces: new[] { "ESHOPPER.Controllers.WebPage" }
    //            );
    //        }
    //    }
    //}
    using System.Web.Mvc;
    using System.Web.Routing;

    namespace ESHOPPER
    {
        public class RouteConfig
        {
            public static void RegisterRoutes(RouteCollection routes)
            {
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 1. Bật Attribute Routing ([Route], [RoutePrefix])
            routes.MapMvcAttributeRoutes();

                // 2. Route cho Admin (ưu tiên cao)
                routes.MapRoute(
                    name: "Admin",
                    url: "Admin/{controller}/{action}/{id}",
                    defaults: new { controller = "Dashboard", action = "index", id = UrlParameter.Optional },
                    namespaces: new[] { "ESHOPPER.Controllers.Admin" }
                );

                // 3. Route cho Auth (ưu tiên cao hơn Default)
                routes.MapRoute(
                    name: "Auth",
                    url: "Auth/{action}/{id}",
                    defaults: new { controller = "Auth", action = "Login", id = UrlParameter.Optional },
                    namespaces: new[] { "ESHOPPER.Controllers.Auth" } // Auth nằm ngoài thư mục
                );

                ////4.Route mặc định cho WebPage(Home, SanPham,...)
                routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    namespaces: new[] { "ESHOPPER.Controllers.WebPage" }
            );
            }
        }
    }   