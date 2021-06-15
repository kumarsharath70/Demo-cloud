using ASPCoreWithAngular.Interfaces;
using ASPCoreWithAngular.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.DataAccess
{
    public class OrderDetailDataAccessLayer : IOrderDetail
    {
        private readonly ILogger<OrderDetailDataAccessLayer> _logger;
        private string connectionString;
        public OrderDetailDataAccessLayer(IConfiguration configuration,  ILogger<OrderDetailDataAccessLayer> logger)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;
        }
        public PODetail GetPurchaseOrderDetail(string fileName)
        {
            PODetail lstPoDetails = new PODetail();
            List<PurchaseOrderDetail> lstOrderDetail = new List<PurchaseOrderDetail>();
            List<PODescription> lstPODescription = new List<PODescription>();
            List<POMessage> lstPOMessage = new List<POMessage>();
            List<OrderDetails> lstPOPart = new List<OrderDetails>();
            List<OrderTOTALs> lstTotals = new List<OrderTOTALs>();
            List<OrderCONDITIONS> lstcond = new List<OrderCONDITIONS>();
            POComment lstComment = new POComment();
            List<OrderMessages> message = new List<OrderMessages>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderREFERENCES", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);
                 

                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                               PurchaseOrderDetail order = new PurchaseOrderDetail();
                                order.Supplier = purchaseOrderReader["Supplier"].ToString();
                                order.OurOrder = purchaseOrderReader["OurOrder"].ToString();
                                order.DateOrder = purchaseOrderReader["DateOrder"].ToString();
                                order.Attention = purchaseOrderReader["Attention"].ToString();
                                order.Buyer = purchaseOrderReader["Buyer"].ToString();
                                lstOrderDetail.Add(order);
                            }
                        }
                    }
                }

               lstPoDetails.PODetails = lstOrderDetail;
               lstPoDetails.PoDescription = GetPODescription(fileName);
                lstPoDetails.PoSupplier = GetPOSupplier(fileName);
                lstPoDetails.POMessage = GetPOMessage(fileName);
                lstPoDetails.POParts = GetPOParts(fileName);
                lstPoDetails.POTotals = GetPOTotal(fileName);
                lstPoDetails.POConditions = GetPOCondition(fileName);
                lstPoDetails.POComment = GetCommentMessage(fileName);
                lstPoDetails.POMessages = GetMessages(fileName);
                return lstPoDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstPoDetails;
            }

        }

        private List<OrderDetails> GetPOParts(string fileName)
        {
           List<OrderDetails> lstparts = new List<OrderDetails>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderDetails", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                OrderDetails part = new OrderDetails();


                                part.OrderDetailsId = Convert.ToInt32( purchaseOrderReader["OrderDetailsId"].ToString());
                                part.FileName =  purchaseOrderReader["FileName"].ToString();
                                part.OrderLineNumber =  purchaseOrderReader["OrderLineNumber"].ToString();
                                part.Quantity =  purchaseOrderReader["Quantity"].ToString();
                                part.Ute =  purchaseOrderReader["Ute"].ToString();

                                part.CodeArticle = purchaseOrderReader["CodeArticle"].ToString();
                                part.Contract = purchaseOrderReader["Contract"].ToString();
                                part.Discount =  purchaseOrderReader["Discount"].ToString();
                                part.UnitPrice =  purchaseOrderReader["UnitPrice"].ToString();
                                part.Unit =  purchaseOrderReader["Unit"].ToString();
                                part.IND =  purchaseOrderReader["IND"].ToString();
                                part.TransicoldContact =  purchaseOrderReader["TransicoldContact"].ToString();
                                part.TotalPrice =  purchaseOrderReader["TotalPrice"].ToString();


                                part.Description = purchaseOrderReader["Description"].ToString();
                                part.SupplierPartCode = purchaseOrderReader["SupplierPartCode"].ToString();
                                part.PlannedDeliveryDate = purchaseOrderReader["PlannedDeliveryDate"].ToString();
                                part.Store = purchaseOrderReader["Store"].ToString();
                                lstparts.Add(part);
                            }
                        }
                    }
                }
                return lstparts;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstparts;
            }

        }

        public List<PODescription> GetPODescription(string fileName)
        {
            List<PODescription> lstOrderDetail = new List<PODescription>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderADRESSELIVRAISON", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                PODescription order = new PODescription();
                                order.CompanyName = purchaseOrderReader["CompanyName"].ToString();
                                order.ContactName = purchaseOrderReader["ContactName"].ToString();
                                order.Address = purchaseOrderReader["Address"].ToString();
                                order.Address2 = purchaseOrderReader["Address2"].ToString();
                                order.City1 = purchaseOrderReader["City1"].ToString();
                                order.City2 = purchaseOrderReader["City2"].ToString();
                                lstOrderDetail.Add(order);
                            }
                        }
                    }
                }
                return lstOrderDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstOrderDetail;
            }

        }

        public List<POSupplier> GetPOSupplier(string fileName)
        {
            List<POSupplier> lstOrderDetail = new List<POSupplier>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderADRESSEFOURNISSEUR", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                POSupplier order = new POSupplier();
                                order.CompanyName = purchaseOrderReader["CompanyName"].ToString();
                                order.ContactName = purchaseOrderReader["ContactName"].ToString();
                                order.Address = purchaseOrderReader["Address"].ToString();
                                order.Address2 = purchaseOrderReader["Address2"].ToString();
                                order.City1 = purchaseOrderReader["City1"].ToString();
                                order.City2 = purchaseOrderReader["City2"].ToString();
                                lstOrderDetail.Add(order);
                            }
                        }
                    }
                }
                return lstOrderDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstOrderDetail;
            }

        }

        public List<POMessage> GetPOMessage(string fileName)
        {
            List<POMessage> lstMessage = new List<POMessage>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderENTETE1AndENTETE2", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                POMessage msg = new POMessage();
                                msg.Message = purchaseOrderReader["Title1"].ToString();
                                
                                lstMessage.Add(msg);
                            }
                        }
                    }
                }
                return lstMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstMessage;
            }

        }

        public List<OrderTOTALs> GetPOTotal(string fileName)
        {
            List<OrderTOTALs> lstTotal = new List<OrderTOTALs>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderTOTAL", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                OrderTOTALs total = new OrderTOTALs();
                                total.Goods = purchaseOrderReader["Goods"].ToString();
                                total.TVA = purchaseOrderReader["TVA"].ToString();
                                total.TotalEUR = purchaseOrderReader["TotalEUR"].ToString();

                                lstTotal.Add(total);
                            }
                        }
                    }
                }
                return lstTotal;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstTotal;
            }

        }

        public List<OrderCONDITIONS> GetPOCondition(string fileName)
        {
            List<OrderCONDITIONS> lstConditions = new List<OrderCONDITIONS>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderCONDITIONS", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                OrderCONDITIONS cond = new OrderCONDITIONS();
                                cond.Delivery = purchaseOrderReader["Delivery"].ToString();
                                cond.City = purchaseOrderReader["City"].ToString();
                                cond.Settlement = purchaseOrderReader["Settlement"].ToString();
                                cond.MethodOfSettlement = purchaseOrderReader["MethodOfSettlement"].ToString();

                                lstConditions.Add(cond);
                            }
                        }
                    }
                }
                return lstConditions;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstConditions;
            }

        }

        public List<OrderMessages> GetMessages(string fileName)
        {
            List<OrderMessages> lstMessage = new List<OrderMessages>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderMessages", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                OrderMessages message = new OrderMessages();

                                message.Message = purchaseOrderReader["Message"].ToString();
                                message.From = purchaseOrderReader["From"].ToString();
                                message.Date = Convert.ToDateTime(purchaseOrderReader["Date"].ToString());
                                lstMessage.Add(message);
                            }
                        }
                    }
                }
                
                return lstMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstMessage;
            }

        }

        public POComment GetCommentMessage(string fileName)
        {
            POComment lstComment = new POComment();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderJURIDIQUE", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileName", fileName);


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                lstComment.Description = purchaseOrderReader["Description"].ToString();
                            }
                        }
                    }
                }
                // lstComment.Comment = GetCommentMessage(fileName);
                return lstComment;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstComment;
            }

        }

        public int AddMessage(string filename, string msg,string userName)
        {
            POComment lstComment = new POComment();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("InsertOrderMessages", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@info_FileName", filename);
                    cmd.Parameters.AddWithValue("@info_Message", msg);
                    cmd.Parameters.AddWithValue("@info_From", userName);
                   


                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                return 1;
                            }
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return 0;
            }

        }
    }
}
