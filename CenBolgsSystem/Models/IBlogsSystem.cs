using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenBolgsSystem.Models
{
    interface IBlogsSystem<T>
    {
        IQueryable SelectData(int page, int limit, string title, int? type = null);

        T ConditionQuery(T id);

        int SelectCount();
       
    }
}
