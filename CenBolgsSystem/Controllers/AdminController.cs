using CenBolgsSystem.App_Start;
using CenBolgsSystem.Models;
using System.Linq;
using System.Web.Mvc;
namespace CenBolgsSystem.Controllers
{
    [LogoinFilter]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult SetAdminPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetAdminPwd(string new_password, string old_password)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                var list = db.Admin.FirstOrDefault(c => c.ad_PassWord == old_password);
                if (list == null)
                {
                    return Json(new { code = 1, msg = "修改失败，旧密码输入错误" }, JsonRequestBehavior.AllowGet);
                }
                list.ad_PassWord = new_password;
                int temp = db.SaveChanges();
                if (temp <= 0)
                {
                    return Json(new { code = 1, msg = "修改失败，请重试" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { code = 0, msg = "修改成功" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}