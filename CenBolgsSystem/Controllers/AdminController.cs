using CenBolgsSystem.App_Start;
using CenBolgsSystem.App_Start.Filters;
using CenBolgsSystem.Models;
using System.Web.Mvc;
namespace CenBolgsSystem.Controllers
{
    [ErrorFilter]
    [LogoinFilter]
    public class AdminController : Controller
    {
        BlogsAdmin Ad = new BlogsAdmin();
        // GET: Admin
        public ActionResult SetAdminPwd()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult EditPower()
        {
            return View();
        }
        public JsonResult GetAdData(int page, int limit, string title)
        {
            return Json(new { code = 0, data = Ad.SelectData(page, limit, title), count = Ad.SelectCount() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetAdminPwd(string new_password, string old_password)
        {
            if (Ad.SetPassWord(new_password, old_password))
            {
                return Json(new { code = 0, msg = "修改成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "修改失败" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetLog()
        {
            return Json(new { code = 0, data = new BlogsAdmin().SelectAdminLog() }, JsonRequestBehavior.AllowGet);
        }
    }
}