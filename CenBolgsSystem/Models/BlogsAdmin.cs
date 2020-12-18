using System;
using System.Linq;

namespace CenBolgsSystem.Models
{
    public class BlogsAdmin : IBlogsOperation<Admin>
    {

        private int num;
        public Admin ConditionQuery(Admin id)
        {
            throw new NotImplementedException();
        }

        public bool InsertData(Admin data)
        {
            throw new NotImplementedException();
        }

        public bool RemoveData(Admin data)
        {
            throw new NotImplementedException();
        }

        public int SelectCount()
        {
            return this.num;
        }

        public IQueryable SelectData(int page, int limit, string title, int? type = null)
        {
            try
            {
                db_CenSystemEntities db = new db_CenSystemEntities();
                if (string.IsNullOrEmpty(title))
                {

                    num = db.Admin.Count();
                    return db.Admin.OrderBy(c => c.ad_Id).Skip((page - 1) * limit).Take(limit).Select(c => new
                    {
                        c.ad_Id,
                        c.ad_UserName,
                        c.AdminType.Ad_TypeName,
                        c.Article.Count,
                        Opercount = c.Operatelog.Count

                    });


                }
                num = db.Admin.Where(c => c.ad_UserName.Contains(title)).Count();
                return db.Admin.OrderBy(c => c.ad_Id).Where(c => c.ad_UserName.Contains(title)).Skip((page - 1) * limit).Take(limit).Select(c => new
                {
                    c.ad_Id,
                    c.ad_UserName,
                    c.AdminType.Ad_TypeName,
                    c.Article.Count,
                    Opercount = c.Operatelog.Count

                });
            }
            catch (Exception)
            {

                return null;
            }

        }

        public bool UpDateData(Admin data)
        {
            throw new NotImplementedException();
        }
        public IQueryable SelectAdminLog()
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            // 查询十条
            return db.Operatelog.OrderByDescending(c => c.log_CreatDataTime).Skip(0).Take(10).Select(c => new
            {
                c.log_Id,
                c.log_CreatDataTime,
                c.Admin.ad_UserName,
                c.OpeStatusType.OpeType_Name,
                c.log_Content,
                c.log_OperAction,
            });
        }
        public bool SetPassWord(string new_password, string old_password)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                var list = db.Admin.FirstOrDefault(c => c.ad_PassWord == old_password);
                if (list == null)
                {
                    return false;
                }
                list.ad_PassWord = new_password;
                return db.SaveChanges() >= 0;
            }
        }
    }
}