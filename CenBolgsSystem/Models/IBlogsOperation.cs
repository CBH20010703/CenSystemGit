using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CenBolgsSystem.Models
{
  
    interface IBlogsOperation<T> 
    {
        bool InsertData(T data, HttpPostedFileBase file);

        bool RemoveData(T data);

        bool UpDateData(T data,HttpPostedFileBase file);
    }
}
