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
    }
}