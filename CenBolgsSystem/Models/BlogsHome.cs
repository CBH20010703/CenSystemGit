using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CenBolgsSystem.Models
{
    public class BlogsHome : IBlogsOperation<Article>
    {
        private int num = 0;
        /// <summary>
        ///  获取返回总行数
        /// </summary>
        /// <returns></returns>
        public int SelectCount()
        {
            return num;
        }
        /// <summary>
        ///   查询指定文章 参数必须与模型id名一样 
        /// </summary>
        /// <param name="data">Article 类型</param>
        /// <returns></returns>
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

        public Array BlogsStatusCount()
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            int[] arr ={
                db.User.Count(),
                db.Article.Count(),
                db.Article.Sum(C=>C.article_PV),
                db.BlogsLeave.Count(),
            };
            return arr;


        }

        /// <summary>
        ///  添加文章 图片上传至Upload 文件夹中
        /// </summary>
        /// <param name="data">Article文章数据</param>
        /// <param name="file">图片</param>
        /// <returns></returns>
        public bool InsertData(Article data, HttpPostedFileBase file)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                try
                {
                    db.Article.Add(data);
                    if (file == null)
                    {
                        return NumberconvertBool(db.SaveChanges());
                    }
                    data.article_ImgUrl = UploadImgs(file);
                    return NumberconvertBool(db.SaveChanges());
                }
                catch (Exception)
                {

                    return NumberconvertBool(db.SaveChanges());
                }
            }
        }
        /// <summary>
        ///  删除文章  删除文章同时删除指定文章的图片
        /// </summary>
        /// <param name="data">Article文章数据</param>
        /// <returns></returns>
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
                            return NumberconvertBool(db.SaveChanges());
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
        /// <summary>
        ///  查询文章可以支持分页 搜索
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="limit">当前条数</param>
        /// <param name="title">搜索内容（可空）</param>
        /// <param name="type">可空的文章类型</param>
        /// <returns></returns>
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
        public bool UpDateData(Article data)
        {
            throw new NotImplementedException();
        }


        public bool RemoveAllData(List<Article> data)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                try
                {
                   
                    foreach (Article item in data)
                    {
                       Article list=db.Article.FirstOrDefault(c => item.article_Id == c.article_Id);
                        //删除文章前需要删除 对应文章的评论
                        if (RemoveArticleLeave(list))
                        {
                            db.Article.Remove(list);
                        }                      
                    }
                    return db.SaveChanges() <= 0 ? false : true;
              
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }

        private bool RemoveArticleLeave(Article art) {
            try
            {
                using (db_CenSystemEntities db = new db_CenSystemEntities())
                {
                    List<BlogsLeave> list = db.BlogsLeave.Where(c => c.leave_articleId == art.article_Id).ToList();
                    if (list == null)
                    {
                        return true;
                    }
                    foreach (var item in list)
                    {
                        db.BlogsLeave.Remove(item);
                    }
                    return db.SaveChanges() <= 0 ? false : true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private bool NumberconvertBool(int num)
        {
            return num <= 0 ? false : true;
        }
        /// <summary>
        ///  更新状态
        /// </summary>
        /// <param name="d">状态以及Id</param>
        /// <returns></returns>

        public bool UpDataArticleField(Article d)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                try
                {
                    db.Article.First(c => c.article_Id == d.article_Id).article_Status = d.article_Status;
                    return NumberconvertBool(db.SaveChanges());
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
        /// <summary>
        /// 编辑文章 如未编辑无法上传 会判断有无替换图片
        /// 替换了就把之前的图片删除掉
        /// </summary>
        /// <param name="d">文章</param>
        /// <param name="file">图片</param>
        /// <returns></returns>
        public int ExitArticle(Article d, HttpPostedFileBase file)
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
                    return db.SaveChanges();
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
                        return db.SaveChanges();
                    }
                    return 0;
                }


            }

        }

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
        private string UploadImgs(HttpPostedFileBase file)
        {
            string ImgUrl = DateTime.Now.ToString("yyyyMMddhhmmssff") + CreateRandomCode(8) + file.FileName.Substring(file.FileName.IndexOf("."));
            string FilePath = System.Web.HttpContext.Current.Server.MapPath("../Upload/") + ImgUrl;
            file.SaveAs(FilePath);
            return "Upload/" + ImgUrl;
        }
        private static bool DeleteFile(string file)
        {
            try
            {
                System.IO.File.Delete(@file);
                //System.IO.File.Delete(@"path");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        bool IBlogsOperation<Article>.InsertData(Article data)
        {
            throw new NotImplementedException();
        }
    }
}