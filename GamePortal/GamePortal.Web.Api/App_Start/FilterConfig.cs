using System.Web;
using System.Web.Mvc;

namespace GamePortal.Web.Api1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
