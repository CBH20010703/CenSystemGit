using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenBolgsSystem.Models
{
    interface IAccount<T>
    {
        bool SetPassWord(string new_password, string old_password);
        bool InsertCcount(T data);

    }
}
