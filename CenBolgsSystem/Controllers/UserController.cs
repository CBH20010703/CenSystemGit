using CenBolgsSystem.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using CenBolgsSystem.App_Start.Filters;
using CenBolgsSystem.App_Start;

namespace CenBolgsSystem.Controllers
{
    public class UserController : Controller
    {
        UserAccount list = new UserAccount();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [LogoinFilter]
        public ActionResult List()
        {
            return View();
        }
        [LogoinFilter]
        public ActionResult EditPower()
        {
            return View();
        }
        public JsonResult UserLogoin(User data)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                if (data.user_Status == true) return Json(new { code = 1, msg = "改账户已被锁定" });
                try
                {
                    User list = db.User.FirstOrDefault(c => c.user_Email == data.user_Email);
                    if (list == null)
                    {
                       
                        return Json(new { code = 1, msg = "账户不存在" });
                    }
                    if (list.user_Email == data.user_Email && list.user_PassWord == data.user_PassWord)
                    {

                        Response.Cookies["UserImg"].Value = list.user_ImgUrl;
                        Response.Cookies["UserEmail"].Value = list.user_Email;
                        Response.Cookies["UserId"].Value = list.user_Id.ToString();
                       
                        return Json(new { code = 0, msg = "登录成功" });
                    }
                    return Json(new { code = 1, msg = "账号或密码不存在" });
                }
                catch (Exception)
                {
                    return Json(new { code = 1, msg = "账户不存在请先注册在登录" });
                }
            }
        }
        public JsonResult InsertUser(User data)
        {
            if (list.InsertCcount(data))
            {
                return Json(new { code = 0, msg = "恭喜你,注册成功!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "注册失败请重新再试" }, JsonRequestBehavior.AllowGet);
        }
        [LogFilter("删除用户", 2)]
        public JsonResult RemoveUser(User data)
        {
            if (data.user_Status == false)
            {
                return Json(new { code = 1, msg = "非锁定状态无法删除" }, JsonRequestBehavior.AllowGet);
            }
            if (list.RemoveData(data))
            {
                return Json(new { code = 0, msg = "删除成功!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SelectUserList(int page, int limit, string title, int? type = null)
        {
            return Json(new { code = 0, data = list.SelectData(page, limit, title, type), count = list.SelectCount() }, JsonRequestBehavior.AllowGet);
        }
        [LogFilter("更新用户状态",1)]
        public JsonResult UserUpdataStatus(User userStatus)
        {
            if (list.UpDataStatus(userStatus))
            {
                return Json(new { code = 0, msg = "状态更新成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "状态更新失败" }, JsonRequestBehavior.AllowGet);
        }
    }
}