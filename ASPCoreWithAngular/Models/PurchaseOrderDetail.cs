using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Models
{
    public class PODetail
    {
       public List<PurchaseOrderDetail> PODetails { get; set; }
        public List<PODescription> PoDescription { get; set; }
        public List<POSupplier> PoSupplier { get; set; }

        public List<POMessage> POMessage { get; set; }
        public List<OrderDetails> POParts { get; set; }
        public List<OrderTOTALs> POTotals { get; set; }
        public List<OrderCONDITIONS> POConditions { get; set; }
        public POComment POComment { get; set; }
        public List<OrderMessages> POMessages { get; set; }
    }
    public class PurchaseOrderDetail
    {
        public string Supplier { get; set; }
        public string OurOrder { get; set; }
        public string DateOrder { get; set; }
        //public string DateEdition { get; set; }
        public string Attention { get; set; }
        public string Buyer { get; set; }
        //public string SalesOrder { get; set; }
        
    }

    public class PODescription
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City1 { get; set; }
        public string City2 { get; set; }
    }

    public class POSupplier
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City1 { get; set; }
        public string City2 { get; set; }
    }


    public class POMessage
    {
        public string Message { get; set; }
    }

    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public string FileName { get; set; }
        public string OrderLineNumber { get; set; }
        public string Quantity { get; set; }
        public string Ute { get; set; }
        public string CodeArticle { get; set; }
        public string Contract { get; set; }
        public string Description { get; set; }
        public string SupplierPartCode { get; set; }
        public string Discount { get; set; }
        public string UnitPrice { get; set; }
        public string Unit { get; set; }
        public string IND { get; set; }
        public string TransicoldContact { get; set; }
        public string TotalPrice { get; set; }
        public string PlannedDeliveryDate { get; set; }
        public string Store { get; set; }
        public string Sig { get; set; }
    }


    public class OrderTOTALs
    {
        public string Goods { get; set; }
        public string TVA { get; set; }
        public string TotalEUR { get; set; }
    }

    public class OrderCONDITIONS
    {
        public string MessageTitle { get; set; }
        public string Delivery { get; set; }
        public string Settlement { get; set; }
        public string MethodOfSettlement { get; set; }
        public string City { get; set; }
    }
    public class POComment
    {
        // public string Comment { get; set; }

        public string Description { get; set; }
    }

    public class OrderMessages
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
        public DateTime Date { get; set; }
    }
}
