using CenBolgsSystem.App_Start;
using CenBolgsSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
namespace CenBolgsSystem.Controllers
{
    [LogoinFilter]
    public class AdminController : Controller
    {
       
        public AdminController()
        {

        }
        BlogsAdmin Ad = new BlogsAdmin();
        // GET: Admin
        public ActionResult SetAdminPwd()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult SetAdminPwd(string new_password, string old_password)
        {
           if(Ad.SetPassWord(new_password, old_password))
            {
                return Json(new { code = 0, msg = "修改成功" }, JsonRequestBehavior.AllowGet);
            }
           return Json(new { code = 1, msg = "修改失败" }, JsonRequestBehavior.AllowGet);

        }
    }
}