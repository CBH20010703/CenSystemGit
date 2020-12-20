using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CenBolgsSystem.Models
{
    public class ArticleSystem : IBlogsOperation<Article>,IBlogsSystem<Article>
    {
        private int num = 0;
        public int SelectCount()
        {
            return num;
        }
        // 上传图片方法 返回上传图片的路径
        private string UploadImgs(HttpPostedFileBase file)
        {
            string ImgUrl = DateTime.Now.ToString("yyyyMMddhhmmssff") + CreateRandomCode(8) + file.FileName.Substring(file.FileName.IndexOf("."));
            string FilePath = System.Web.HttpContext.Current.Server.MapPath("../Upload/") + ImgUrl;
            file.SaveAs(FilePath);
            return "Upload/" + ImgUrl;
        }
        //创建随机图片名称
        private string CreateRandomCode(int length)
        {
            string[] codes = new string[36] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            StringBuilder randomCode = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                randomCode.Append(codes[rand.Next(codes.Length)]);
            }
            return randomCode.ToString();
        }
        //删除图片
        private static bool DeleteFile(string file)
        {
            try
            {
                System.IO.File.Delete(@file);
                //System.IO.File.Delete(@"path");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        // Select 单条 文章
        public Article ConditionQuery(Article data)
        {
            if (data == null)
            {
                return null;
            }
            db_CenSystemEntities db = new db_CenSystemEntities();
            db.Configuration.ProxyCreationEnabled = false;
            return db.Article.FirstOrDefault(c => c.article_Id == data.article_Id);
        }
        // 插入文章 和图片
        public bool InsertData(Article data,HttpPostedFileBase file)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                try
                {
                    db.Article.Add(data);
                    if (file == null)
                    {
                        return db.SaveChanges() >= 0;
                    }
                    data.article_ImgUrl = UploadImgs(file);
                    return db.SaveChanges() >= 0;
                }
                catch (Exception)
                {

                    return db.SaveChanges() >= 0;
                }
            }
        }
        // 删除文章 和图片
        public bool RemoveData(Article data)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {

                try
                {
                    var list = db.Article.FirstOrDefault(c => c.article_Id == data.article_Id);
                    //判断是否把之前的图片删除完毕
                    if (DeleteFile(HttpContext.Current.Server.MapPath("../") + list.article_ImgUrl))
                    {
                        //判断是否把文章对应留言删除完毕
                        if (RemoveArticleLeave(list))
                        {
                            // 执行删除文章
                            db.Article.Remove(list);
                            return db.SaveChanges()>0;
                        }
                        //返回状态
                        return false;
                    }
                    return false;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }
        // 多选删除文章和图片
        public bool RemoveAllData(List<Article> data)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                try
                {

                    foreach (Article item in data)
                    {
                        Article list = db.Article.FirstOrDefault(c => item.article_Id == c.article_Id);
                        //删除文章前需要删除 对应文章的评论
                        if (RemoveArticleLeave(list)&&DeleteFile(HttpContext.Current.Server.MapPath("../") + list.article_ImgUrl))
                        {
                            db.Article.Remove(list);
                        }
                    }
                    return db.SaveChanges() >= 0;

                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
        private bool RemoveArticleLeave(Article art)
        {
            try
            {
                using (db_CenSystemEntities db = new db_CenSystemEntities())
                {
                    List<BlogsLeave> list = db.BlogsLeave.Where(c => c.leave_articleId == art.article_Id).ToList();
                    if (list.Count == 0) return true;
                    foreach (var item in list)
                    {
                        db.BlogsLeave.Remove(item);
                    }
                    return db.SaveChanges() >= 0;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        // 统计 
        public Array BlogsStatusCount()
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            // 判断在没有文章的情况下
            int Num;
            if (db.Article.Count() == 0)
            {
                Num = 0;
            }
            else
            {
                Num = db.Article.Sum(c => c.article_PV);
            }
             


            int?[] arr ={
                db.User.Count(),
                db.Article.Count(),
                Num,
                db.BlogsLeave.Count(),
            };
            return arr;
        }
        // 分页 加搜索查询
        public IQueryable SelectData(int page, int limit, string title, int? type = null)
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            if (string.IsNullOrEmpty(title))
            {
                if (type == 0 || type == null)
                {
                    num = db.Article.Count();
                    return db.Article.OrderByDescending(c => c.article_Id).Skip((page - 1) * limit).Take(limit).Select(c => new
                    {
                        c.ArticleType.type_Name,
                        c.article_Content,
                        c.article_CreatDateTime,
                        c.article_ImgUrl,
                        c.article_Status,
                        c.article_Title,
                        c.article_Id,
                        c.Admin.ad_UserName,
                        c.article_PV
                    });
                }
                num = db.Article.Where(c => c.article_Type == type).Count();
                return db.Article.OrderByDescending(c => c.article_Id).Where(c => c.article_Type == type).Skip((page - 1) * limit).Take(limit).Select(c => new
                {
                    c.ArticleType.type_Name,
                    c.article_Content,
                    c.article_CreatDateTime,
                    c.article_ImgUrl,
                    c.article_Status,
                    c.article_Title,
                    c.article_Id,
                    c.Admin.ad_UserName,
                    c.article_PV
                });
            }
            num = db.Article.Where(c => c.article_Title.Contains(title)).Count();
            return db.Article.OrderByDescending(c => c.article_Id).Where(c => c.article_Title.Contains(title)).Skip((page - 1) * limit).Take(limit).Select(c => new
            {
                c.ArticleType.type_Name,
                c.article_Content,
                c.article_CreatDateTime,
                c.article_ImgUrl,
                c.article_Status,
                c.article_Title,
                c.article_Id,
                c.Admin.ad_UserName,
                c.article_PV
            });
        }
        // 更新数据
        public bool UpDateData(Article d,HttpPostedFileBase file)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                Article queryList = db.Article.First(c => c.article_Id == d.article_Id);

                if (file == null)
                {

                    queryList.article_Content = d.article_Content;
                    queryList.article_Type = d.article_Type;
                    queryList.article_Title = d.article_Title;
                    queryList.article_MakHtml = d.article_MakHtml;
                    queryList.article_MakCode = d.article_MakCode;
                    return db.SaveChanges()>0;
                }
                else
                {
                    if (DeleteFile(HttpContext.Current.Server.MapPath("../") + d.article_ImgUrl))
                    {
                        queryList.article_Content = d.article_Content;
                        queryList.article_Type = d.article_Type;
                        queryList.article_Title = d.article_Title;
                        queryList.article_MakHtml = d.article_MakHtml;
                        queryList.article_MakCode = d.article_MakCode;
                        queryList.article_ImgUrl = UploadImgs(file);
                        return db.SaveChanges()>0;
                    }
                    return false;
                }
            }
        }
        // 更新字段
        public bool UpDataArticleField(Article d)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                try
                {
                    db.Article.First(c => c.article_Id == d.article_Id).article_Status = d.article_Status;
                    return db.SaveChanges()>0;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
    }
}