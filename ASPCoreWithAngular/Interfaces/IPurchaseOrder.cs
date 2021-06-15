using ASPCoreWithAngular.Models;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Interfaces
{
    public interface IPurchaseOrder
    {
        IEnumerable<PurchaseOrder> GetAllPurchaseOrder();
        IWorkbook ExportUsersToExcel();
    }
}
