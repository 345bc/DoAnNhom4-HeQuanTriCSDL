using System.Web;
using System.Web.Mvc;

namespace QlyDonHang_DoAn_hqtcsdl
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
