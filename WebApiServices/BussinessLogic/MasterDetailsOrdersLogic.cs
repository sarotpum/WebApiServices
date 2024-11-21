using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedService.Contracts;
using SharedService.Implements;
using SharedService.LogProvider.Interface;
using SharedService.Models;
using SharedService.Models.MasterDetailsOrders;
using System.Data;
using System.Data.SqlClient;

namespace WebApiServices.BussinessLogic
{
    public class MasterDetailsOrdersLogic
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILoggerService _logger;
        private readonly string _connectString;

        public MasterDetailsOrdersLogic(IOptions<AppSettings> options, ILoggerService logger)
        {
            _logger = logger;
            _options = options;
            _connectString = _options.Value.databaseSetting.ConnectionString;
        }

        public JsonResult GetItemDetails()
        { 
            _logger.LogInfo("GetItemDetails", Constant.IN, typeof(MasterDetailsOrdersLogic).Name);

            DataTable table = new DataTable();
            SqlDataReader myReader;

            try
            {
                using (SqlConnection objConn = new SqlConnection(_connectString))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        objConn.Open();
                        objCmd.Connection = objConn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = StringSqlImpl.GetItemDetails();

                        myReader = objCmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }
                
                return new JsonResult(table);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception GetItemDetails", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("GetItemDetails", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult GetCustomerDetails()
        {
            _logger.LogInfo("GetCustomerDetails", Constant.IN, typeof(MasterDetailsOrdersLogic).Name);

            DataTable table = new DataTable();
            SqlDataReader myReader;

            try
            {
                using (SqlConnection objConn = new SqlConnection(_connectString))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        objConn.Open();
                        objCmd.Connection = objConn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = StringSqlImpl.GetCustomerDetails();

                        myReader = objCmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }

                return new JsonResult(table);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception GetCustomerDetails", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("GetCustomerDetails", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult AddOrdersDetail(OrdersDetailsModel order)
        {
            _logger.LogInfo("AddOrdersDetail", Constant.IN, typeof(MasterDetailsOrdersLogic).Name);

            try
            {
                using (SqlConnection objConn = new SqlConnection(_connectString))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        objConn.Open();
                        objCmd.Connection = objConn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = StringSqlImpl.AddOrderDetail(order);
                        objCmd.ExecuteNonQuery();

                        objCmd.CommandText = StringSqlImpl.MaxOrder();
                        int id = Convert.ToInt16(objCmd.ExecuteScalar());

                        if (order.OrderItems.Count > 0)
                        {
                            foreach (var item in order.OrderItems)
                            {
                                objCmd.Parameters.Clear();
                                objCmd.CommandText = StringSqlImpl.AddOrdersDetailItems();
                                objCmd.Parameters.AddWithValue("@OrderID", id);
                                objCmd.Parameters.AddWithValue("@ItemID", item.ItemID);
                                objCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                objCmd.ExecuteNonQuery();
                            }
                        }

                        objConn.Close();
                    }
                }

                return new JsonResult("Added Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception AddOrdersDetail", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                throw;
            }
            finally
            {
                _logger.LogInfo("AddOrdersDetail", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult GetOrdersDetails()
        {
            _logger.LogInfo("GetOrdersDetails", Constant.IN, typeof(MasterDetailsOrdersLogic).Name);

            DataTable table = new DataTable();
            SqlDataReader myReader;

            try
            {
                using (SqlConnection objConn = new SqlConnection(_connectString))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        objConn.Open();
                        objCmd.Connection = objConn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = StringSqlImpl.GetOrdersDetail();

                        myReader = objCmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }

                return new JsonResult(table);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception GetOrdersDetails", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                throw;
            }
            finally
            {
                _logger.LogInfo("GetOrdersDetails", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public (FormData, List<OrderItems>) GetOrdersDetail(long id)
        {
            _logger.LogInfo("GetOrdersDetail", Constant.IN, typeof(MasterDetailsOrdersLogic).Name);

            DataSet ds = new DataSet();
            FormData order = new FormData();
            List<OrderItems> orderItems = new List<OrderItems>();

            try
            {
                using (SqlConnection objConn = new SqlConnection(_connectString))
                {
                    objConn.Open();

                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                        {
                            objCmd.Connection = objConn;
                            objCmd.CommandType = CommandType.Text;
                            objCmd.CommandText = StringSqlImpl.GetOrdersDetail(id);
                            dataAdapter.SelectCommand = objCmd;
                            dataAdapter.Fill(ds);

                            foreach (DataTable myTable in ds.Tables)
                            {
                                foreach (DataRow myRow in myTable.Rows)
                                {
                                    //order.OrderID = (int)myRow["OrderID"];
                                    order.OrderNo = myRow["OrderNo"].ToString() ?? "";
                                    order.CustomerID = (int)myRow["CustomerID"];
                                    order.PMethod = myRow["PMethod"].ToString() ?? "";
                                    order.GTotal = double.Parse(myRow["OrderID"].ToString() ?? "0");
                                }
                            }
                            ds.Clear();
                            dataAdapter.Dispose(); 
                        }
                    }

                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                        {
                            objCmd.Connection = objConn;
                            objCmd.CommandText = StringSqlImpl.GetOrdersItems(id);
                            objCmd.CommandType = CommandType.Text;
                            dataAdapter.SelectCommand = objCmd;
                            dataAdapter.Fill(ds);
                            foreach (DataTable myTable in ds.Tables)
                            {
                                foreach (DataRow myRow in myTable.Rows)
                                {
                                    orderItems.Add(new OrderItems()
                                    {
                                        OrderID = Int32.Parse(myRow["OrderID"].ToString() ?? "0"),
                                        OrderItemID = Int32.Parse(myRow["OrderItemID"].ToString() ?? "0"),
                                        ItemID = Int32.Parse(myRow["ItemID"].ToString() ?? "0"),
                                        ItemName = myRow["ItemName"].ToString() ?? "",
                                        Price = double.Parse(myRow["Price"].ToString() ?? "0"),
                                        Quantity = Int32.Parse(myRow["Quantity"].ToString() ?? "0"),
                                        Total = double.Parse(myRow["Total"].ToString() ?? "0")
                                    });
                                }
                            }
                            ds.Clear();
                            dataAdapter.Dispose();
                        }
                    }

                }

                return (order, orderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception GetOrdersDetail", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                throw;
            }
            finally
            {
                _logger.LogInfo("GetOrdersDetail", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }
        
        public JsonResult DeleteOrder(long id)
        { 
            _logger.LogInfo("DeleteOrder", Constant.IN, typeof(MasterDetailsOrdersLogic).Name);
              
            try
            {
                using (SqlConnection objConn = new SqlConnection(_connectString))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        objConn.Open();
                        objCmd.Connection = objConn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = StringSqlImpl.DeleteOrderDetal(id);
                        objCmd.ExecuteNonQuery();
                        objCmd.Parameters.Clear();

                        objCmd.CommandText = StringSqlImpl.DeleteOrderItems(id);
                        objCmd.ExecuteNonQuery();
                        objCmd.Parameters.Clear();
                        objConn.Close();
                    }
                }

                return new JsonResult("Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception DeleteOrder", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult("Error Deleted");
            }
            finally
            {
                _logger.LogInfo("DeleteOrder", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }
    }
}
