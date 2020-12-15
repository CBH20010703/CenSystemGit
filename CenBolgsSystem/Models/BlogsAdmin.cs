using System;
using System.Linq;

namespace CenBolgsSystem.Models
{
    public class BlogsAdmin : IBlogsOperation<Admin>
    {
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
            throw new NotImplementedException();
        }

        public IQueryable SelectData(int page, int limit, string title, int? type = null)
        {
            throw new NotImplementedException();
        }

        public bool UpDateData(Admin data)
        {
            throw new NotImplementedException();
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
             return  db.SaveChanges() <= 0 ? false : true;
            }
        }
    }
}