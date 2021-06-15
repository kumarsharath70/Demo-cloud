using ASPCoreWithAngular.Interfaces;
using ASPCoreWithAngular.Models;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrder objPurchase;

        public PurchaseOrderController(IPurchaseOrder _objpurchase)
        {
            objPurchase = _objpurchase;
        }

        [HttpGet]
        [Route("Index")]
        public IEnumerable<PurchaseOrder> Index()
        {
            return objPurchase.GetAllPurchaseOrder();
        }


        [HttpGet]
        [Route("ExportToExcel")]
        public IActionResult ExportToExcel()
        {
            IWorkbook workbook = objPurchase.ExportUsersToExcel();
            string contentType = ""; // Scope
            // Credit for two stream since workbook.write() closes the first one: https://stackoverflow.com/a/36584861/6336270 
            MemoryStream tempStream = null;
            MemoryStream stream = null;
            try
            {
                // 1. Write the workbook to a temporary stream
                tempStream = new MemoryStream();
                workbook.Write(tempStream);
                // 2. Convert the tempStream to byteArray and copy to another stream
                var byteArray = tempStream.ToArray();
                stream = new MemoryStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Seek(0, SeekOrigin.Begin);
                // 3. Set file content type
                contentType = workbook.GetType() == typeof(XSSFWorkbook) ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" : "application/vnd.ms-excel";
                // 4. Return file
                return File(
                    fileContents: stream.ToArray(),
                    contentType: contentType,
                    fileDownloadName: "OrderListExcel " + DateTime.Now.ToString() + ".xlsx");
            }
            finally
            {
                if (tempStream != null) tempStream.Dispose();
                if (stream != null) stream.Dispose();
            }
        }
    }
}
