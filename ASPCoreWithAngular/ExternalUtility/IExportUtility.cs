using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.ExternalUtility
{
    public interface IExportUtility
    {
        IWorkbook WriteExcelWithNPOI<T>(List<T> data, string extension);
    }
}
