using ASPCoreWithAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Interfaces
{
    public interface IOrderDetail
    {
       PODetail GetPurchaseOrderDetail(string fileName);
        int AddMessage(string filename, string msg, string userName);
    }
}
