using ASPCoreWithAngular.ExternalUtility;
using ASPCoreWithAngular.Interfaces;
using ASPCoreWithAngular.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.DataAccess
{
    public class PurchaseOrderAccessLayer: IPurchaseOrder
    {
        private string connectionString;
        private int pageSize = 0;
        private IExportUtility _exportUtility { get; set; }
        private readonly ILogger<PurchaseOrderAccessLayer> _logger;
        public PurchaseOrderAccessLayer(IConfiguration configuration, IExportUtility exportUtility, ILogger<PurchaseOrderAccessLayer> logger)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
            pageSize = Convert.ToInt32(configuration["Variables:PageSize"]);
            _exportUtility = exportUtility;
            _logger = logger;
        }

       public IEnumerable<PurchaseOrder> GetAllPurchaseOrder()
        {
            List<PurchaseOrder> lstemployee = new List<PurchaseOrder>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetPurchaseOrderDeatilsForCarrierUser", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@SupplierCode", 0);// 0 is for admin supplier code
                    cmd.Parameters.AddWithValue("@PageNum", 1); 
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@sortColumnName", "Ordernr");
                    cmd.Parameters.AddWithValue("@sortDirection", "ASC");

                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                PurchaseOrder order = new PurchaseOrder();
                                Actions act = new Actions();
                                act = FillOrderActions(purchaseOrderReader["FileName"].ToString());
                                order.Action = act.ActionName;
                                order.Approved = act.ApprovedFlag;
                                order.OrderNo = Convert.ToInt32(purchaseOrderReader["Ordernr"]);
                                order.FileName = purchaseOrderReader["FileName"].ToString();
                                order.PlannerName = purchaseOrderReader["PlannerName"].ToString();
                                order.Location = purchaseOrderReader["Location"].ToString() == "500" ?"CTE":"CTI";
                                order.Email = purchaseOrderReader["EmailID"].ToString();
                                order.Date = purchaseOrderReader["Date"].ToString();
                                order.SupplierName = purchaseOrderReader["SupplierName"].ToString()
        ;
                                lstemployee.Add(order);
                            }
                        }
                    }
                }
                return lstemployee;
            }
            catch(Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstemployee;
            }
        }

        public string FillOrderActions1(string filename)
        {
            string action = " ";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderStatus", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fileName", filename);

                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                if (!string.IsNullOrEmpty(purchaseOrderReader["Read"].ToString())
                                    && !string.IsNullOrEmpty(purchaseOrderReader["Message"].ToString()))
                                {
                                    action = "Message";
                                }
                                else if (!string.IsNullOrEmpty(purchaseOrderReader["Read"].ToString()))
                                {
                                    if(Convert.ToBoolean(purchaseOrderReader["Read"].ToString()))
                                    {
                                        action = "Read";
                                    }
                                    else
                                    {
                                        action = string.Empty;
                                    }
                                    
                                }
                                else if (!string.IsNullOrEmpty(purchaseOrderReader["Message"].ToString()))
                                {
                                    action = "Message";
                                }
                                else 
                                {
                                    action = string.Empty;
                                }
                            }
                        }
                    }
                }
                return action;
            }
            catch(Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return action;
            }
        }

        public Actions FillOrderActions(string filename)
        {
            string action = " ";
            Actions lstAction = new Actions();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetOrderStatus", con);// carrier user
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fileName", filename);

                    using (SqlDataReader purchaseOrderReader = cmd.ExecuteReader())
                    {
                        if (purchaseOrderReader != null)
                        {
                            while (purchaseOrderReader.Read())
                            {
                                if (!string.IsNullOrEmpty(purchaseOrderReader["Read"].ToString())
                                    && !string.IsNullOrEmpty(purchaseOrderReader["Message"].ToString()))
                                {
                                    action = "Message";
                                }
                                else if (!string.IsNullOrEmpty(purchaseOrderReader["Read"].ToString()))
                                {
                                    if (Convert.ToBoolean(purchaseOrderReader["Read"].ToString()))
                                    {
                                        action = "Read";
                                    }
                                    else
                                    {
                                        action = string.Empty;
                                    }

                                }
                                else if (!string.IsNullOrEmpty(purchaseOrderReader["Message"].ToString()))
                                {
                                    action = "Message";
                                }
                                else
                                {
                                    action = string.Empty;
                                }

                                lstAction.ActionName = action;
                                lstAction.ApprovedFlag = Convert.ToBoolean(purchaseOrderReader["Read"].ToString()) ? "Approved" : "Not Approved"; 
                            }
                        }
                    }
                }
                return lstAction;
            }
            catch (Exception ex)
            {
                _logger.LogError(" " + ex.ToString());
                return lstAction;
            }
        }
        public IWorkbook ExportUsersToExcel()
        {
            List<PurchaseOrder> orders = new List<PurchaseOrder>();
            // 1. Get all orders
            orders  = (List<PurchaseOrder>)GetAllPurchaseOrder();
            // 2. Return users Excel workbook
            return _exportUtility.WriteExcelWithNPOI(orders, "xlsx");
        }
    }
}
