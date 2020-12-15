using CenBolgsSystem.App_Start;
using CenBolgsSystem.App_Start.Filters;
using CenBolgsSystem.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CenBolgsSystem.Controllers
{
    [ErrorFilter]
    [LogoinFilter]
    public class HomeController : Controller
    {
        BlogsHome list = new BlogsHome();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditArticle()
        {
            return View();
        }

        public ActionResult UpDateArticle()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        [ValidateInput(false)]
        public JsonResult InsertArticle(Article data, HttpPostedFileBase file)
        {
            if (list.InsertData(data, file))
            {
                return Json(new { code = 0, msg = "上传成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "上传失败" }, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public JsonResult Exit(Article data, HttpPostedFileBase file)
        {
            if (list.ExitArticle(data, file) <= 0)
            {
                return Json(new { code = 1, msg = "上传失败" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 0, msg = "上传成功" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetoneArticle(Article d)
        {
            return Json(new { code = 0, data = list.ConditionQuery(d) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpDataArticleStatus(Article d)
        {
            if (!list.UpDataArticleField(d))
            {
                return Json(new { code = 1, msg = "更新状态失败" }, JsonRequestBehavior.AllowGet);
            };
            return Json(new { code = 0, msg = "更新状态成功" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WelComeCount()
        {
           
            return Json(new { data = list.BlogsStatusCount() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteArticle(Article d)
        {
            if (d.article_Status == true)
            {
                return Json(new { code = 1, msg = "删除失败 锁定状态无法删除" }, JsonRequestBehavior.AllowGet);
            }
            if (list.RemoveData(d))
            {
                return Json(new { code = 0, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAllArticle(List<Article> data)
        {
            if (list.RemoveAllData(data))
            {
                return Json(new { code = 0, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult SelectArticle(int page, int limit, string title, int? type = null)
        {
            return Json(new { code = 0, data = list.SelectData(page, limit, title, type), count = list.SelectCount(), }, JsonRequestBehavior.AllowGet);
        }
    }
}