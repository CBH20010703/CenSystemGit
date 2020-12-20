using CenBolgsSystem.Models;
using System.Linq;
using System.Web.Mvc;

namespace CenBolgsSystem.Controllers
{
    public class BlogsController : Controller
    {
        // GET: Blogs
        BlogsArticle list = new BlogsArticle();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult blogs()
        {
            return View();
        }

        public ActionResult Read()
        {
            return View();
        }

        public JsonResult GetArticleList(int page, int limit, string title, int datatype)
        {
            return Json(new
            {
                code = 0,
                data = list.SelectData(page, limit, title, datatype),
                count = list.SelectCount(),
                datatype,
                page
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserAddLeave(BlogsLeave data)
        {
            if (list.AddLeave(data))
            {
                return Json(new { code = 0, msg = "留言成功" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 1, msg = "留言失败" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReadArticle(Article d)
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            var datalist = db.Article.FirstOrDefault(c => c.article_Id == d.article_Id);
            if (datalist == null)
            {
                return Json(new { code = 1, msg = "Error" },JsonRequestBehavior.AllowGet);
            }
            datalist.article_PV += 1;
            db.SaveChanges();
            return Json(new { code = 0, data = list.ConditionQuery(d) }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SelectArticleLeave(BlogsLeave data)
        {
            return Json(new { code = 0, data = list.SelectLeave(data) }, JsonRequestBehavior.AllowGet);
        }
    }
}