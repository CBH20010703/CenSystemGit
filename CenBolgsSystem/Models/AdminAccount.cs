using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenBolgsSystem.Models
{
    public class AdminAccount : IAccount<Admin>, IBlogsSystem<Admin>
    {
        private int num;
        public Admin ConditionQuery(Admin data)
        {
            if (data == null)
            {
                return null;
            }
            db_CenSystemEntities db = new db_CenSystemEntities();
            db.Configuration.ProxyCreationEnabled = false;

            return db.Admin.FirstOrDefault(c => c.ad_Id == data.ad_Id);
        }
        public bool RemoveData(Admin data)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                Admin list = db.Admin.FirstOrDefault(c => c.ad_Id == data.ad_Id);
                if (RemoveLog(list))
                {
                    if (list == null)
                    {
                        return false;
                    }
                    db.Admin.Remove(list);
                    return db.SaveChanges() > 0;
                }
                return false;
               
            }
        }
        private bool RemoveLog(Admin data)
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
          var list= db.Operatelog.Where(c => c.log_OperatelogAdmin == data.ad_Id).ToList();
            if (list.Count == 0) return true;
            foreach (var item in list)
            {
                db.Operatelog.Remove(item);
            }
            return db.SaveChanges() > 0;
        }
        public int SelectCount()
        {
            return num;
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

        public IQueryable SelectAdminLog()
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            // 查询15条
            return db.Operatelog.OrderByDescending(c => c.log_CreatDataTime).Skip(0).Take(15).Select(c => new
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
                return db.SaveChanges() > 0;
            }
        }
      
        public bool InsertCcount(Admin data)
        {
            try
            {
                using(db_CenSystemEntities db=new db_CenSystemEntities())
                {
                    data.ad_Type = 2;
                    db.Admin.Add(data);
                    return db.SaveChanges()> 0;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

       
    }
}