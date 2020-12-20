using CenBolgsSystem.Models;
using System.Linq;
using System.Web.Mvc;
namespace CenBolgsSystem.Controllers
{
    public class LogoinController : Controller
    {
        // GET: Logoin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(Admin d)
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            var list = db.Admin.FirstOrDefault(c => c.ad_UserName == d.ad_UserName && c.ad_PassWord == d.ad_PassWord);
            if (list != null)
            {

                Response.Cookies["AdminName"].Value = list.ad_UserName;
                Response.Cookies["AdminId"].Value = list.ad_Id.ToString();
                Session["AdminName"] = list.ad_UserName;
                Session["AdminId"] = list.ad_Id;
                return Json(new { code = 0, msg = "登录成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "登录失败" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QiutLogoin()
        {
            Session["AdminName"] = null;
            return Json(new { code = 0, msg = "清除成功" }, JsonRequestBehavior.AllowGet);
        }
    }
}