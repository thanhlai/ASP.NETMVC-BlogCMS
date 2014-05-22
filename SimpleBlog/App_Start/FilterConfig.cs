using SimpleBlog.Infastructure;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TransactionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}