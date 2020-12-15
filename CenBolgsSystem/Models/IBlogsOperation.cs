using System.Linq;

namespace CenBolgsSystem.Models
{
    interface IBlogsOperation<T>
    {
        bool InsertData(T data);

        bool RemoveData(T data);

        IQueryable SelectData(int page, int limit, string title, int? type = null);

        bool UpDateData(T data);

        T ConditionQuery(T id);

        int SelectCount();
    }
}
