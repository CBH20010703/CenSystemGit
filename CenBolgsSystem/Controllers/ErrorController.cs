using CenBolgsSystem.Models;
using System.Linq;
using System.Web.Mvc;
namespace CenBolgsSystem.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetErrorLogList()
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            return Json(new
            {
                code = 0,
                data = db.ErrorLog.OrderByDescending(c => c.error_createdatetime).Skip(0).Take(6),
                msg = "查询成功"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}