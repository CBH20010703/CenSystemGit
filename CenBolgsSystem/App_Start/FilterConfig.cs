using CenBolgsSystem.App_Start.Filters;
using System.Web.Mvc;

namespace CenBolgsSystem.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogoinFilterAttribute());
            filters.Add(new ErrorFilterAttribute());
        }
    }
}