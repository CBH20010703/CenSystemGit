using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenBolgsSystem.Models
{
    public class AritlceBolgs : IBlogsSystem<Article>
    {   
        // 查询留言
        public IQueryable SelectLeave(BlogsLeave data)
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            return db.BlogsLeave.Where(c => c.leave_articleId == data.leave_articleId).Select(c => new
            {
                c.User.user_Name,
                c.leave_content,
                c.leave_creatdatetime,
                c.User.user_ImgUrl,
                c.Article.Admin.ad_UserName,
            }); ;
        }
        // 添加留言
        public bool AddLeave(BlogsLeave data)
        {
            try
            {
                db_CenSystemEntities db = new db_CenSystemEntities();
                data.leave_creatdatetime = DateTime.Now;
                db.BlogsLeave.Add(data);
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        ///  查询单条文章操作
        /// </summary>
        private int num;
        public Article ConditionQuery(Article id)
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.SaveChanges();
            return db.Article.FirstOrDefault(c => c.article_Id == id.article_Id);
        }

        public int SelectCount()
        {
            return num;
        }
        // 查询前台文章 
        public IQueryable SelectData(int page, int limit, string title, int? type = null)
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            if (string.IsNullOrEmpty(title))
            {
                if (type == 0)
                {
                    num = db.Article.Count();

                    return db.Article.OrderByDescending(c => c.article_Id).Where(c => c.article_Status == false).Skip((page - 1) * limit).Take(limit).Select(c => new
                    {
                        c.article_Title,
                        c.article_CreatDateTime,
                        c.article_ImgUrl,
                        c.article_Content,
                        c.article_Id,
                        c.Admin.ad_UserName,
                        c.ArticleType.type_Name,
                        c.article_PV,
                        c.BlogsLeave.Count,
                    });
                }
                num = db.Article.Where(c => c.article_Type == type).Count();
                return db.Article.OrderByDescending(c => c.article_Id).Where(c => c.article_Type == type && c.article_Status == false).Skip((page - 1) * limit).Take(limit).Select(c => new
                {
                    c.article_Title,
                    c.article_CreatDateTime,
                    c.article_ImgUrl,
                    c.article_Content,
                    c.article_Id,
                    c.Admin.ad_UserName,
                    c.ArticleType.type_Name,
                    c.article_PV,
                    c.BlogsLeave.Count,
                });
            }
            num = db.Article.Where(c => c.article_Title.Contains(title)).Count();
            return db.Article.OrderByDescending(c => c.article_Id).Where(c => c.article_Title.Contains(title) && c.article_Status == false).Skip((page - 1) * limit).Take(limit).Select(c => new
            {
                c.article_Title,
                c.article_CreatDateTime,
                c.article_ImgUrl,
                c.article_Content,
                c.article_Id,
                c.Admin.ad_UserName,
                c.ArticleType.type_Name,
                c.article_PV,
                c.BlogsLeave.Count,
            });
        }
    }
}