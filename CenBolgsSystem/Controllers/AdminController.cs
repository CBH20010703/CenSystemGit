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
 
        AdminAccount Ad = new AdminAccount();
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
        [LogFilter("修改管理员密码",3)]
        public ActionResult SetAdminPwd(string new_password, string old_password)
        {
            if (Ad.SetPassWord(new_password, old_password))
            {
                return Json(new { code = 0, msg = "修改成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "修改失败" }, JsonRequestBehavior.AllowGet);
        }
        [LogFilter("添加管理员", 3)]
        public ActionResult AddAdmin(Admin data)
        {
           
            if (Ad.InsertCcount(data))
            {
                return Json(new { code = 0, msg = "添加成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 0, msg = "添加失败" }, JsonRequestBehavior.AllowGet);
        }
        [LogFilter("删除管理员", 3)]
        public JsonResult RemoveAdmin(Admin d)
        {
            if (d.ad_Id == 1)
            {
                return Json(new { code = 1, msg = "无法删除超级管理员" }, JsonRequestBehavior.AllowGet);
            }
            if (Ad.RemoveData(d))
            {
                return Json(new { code = 0, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "删除失败！ Eroor" }, JsonRequestBehavior.AllowGet);
        
    }
        public ActionResult GetLog()
        {
            return Json(new { code = 0, data =Ad.SelectAdminLog() }, JsonRequestBehavior.AllowGet);
        }
    }
}