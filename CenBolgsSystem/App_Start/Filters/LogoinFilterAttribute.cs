using System.Web.Mvc;

namespace CenBolgsSystem.App_Start
{
    public class LogoinFilterAttribute : ActionFilterAttribute
    {
        public bool IsCheck { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (filterContext.HttpContext.Session["AdminName"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Logoin/Index");
            }

        }
    }
}