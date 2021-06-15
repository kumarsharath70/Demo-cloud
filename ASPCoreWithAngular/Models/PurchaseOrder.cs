using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Models
{
    public class PurchaseOrder
    {
        public string Action { get; set; }
        public string Date { get; set; }
        public string FileName { get; set; }
        public string PlannerName { get; set; }

        public int OrderNo { get; set; }
        public string SupplierName { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }

        public string Approved { get; set; }
    }
}
