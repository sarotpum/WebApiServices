using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedService.Contracts;
using SharedService.Implements;
using SharedService.LogProvider.Interface;
using SharedService.Models;
using SharedService.Models.Products;
using System.Data;
using System.Data.SqlClient;

namespace WebApiServices.BussinessLogic
{
    public class ProductsLogic
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILoggerService _logger;
        private readonly string _connectString;

        public ProductsLogic(IOptions<AppSettings> optins, ILoggerService logger)
        {
            _logger = logger;
            _options = optins;
            _connectString = _options.Value.databaseSetting.ConnectionString;
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            _logger.LogInfo("GetProducts", Constant.IN, typeof(DepartmentLogic).Name);

            List<ProductModel> result = new List<ProductModel>();
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
                        objCmd.CommandText = StringSqlImpl.GetProducts();

                        myReader = await objCmd.ExecuteReaderAsync();
                        
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                result.Add(new ProductModel()
                                {
                                    id = myReader.GetInt32(0),
                                    name = myReader.GetString(1),
                                    description = myReader.GetString(2),
                                    price = myReader.GetString(3)
                                });
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception GetProducts", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return result;
            }
            finally
            {
                _logger.LogInfo("GetProducts", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public async Task<ActionResult<ProductModel>> AddProduct(ProductModel value)
        {
            _logger.LogInfo("AddProduct", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText = StringSqlImpl.AddProduct(value);

                        myReader = await objCmd.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }

                return new JsonResult("Added Product Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception AddProduct", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("AddProduct", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public async Task<ActionResult<ProductModel>> UpdateProduct(ProductModel value)
        {
            _logger.LogInfo("UpdateProduct", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandType= CommandType.Text;
                        objCmd.CommandText= StringSqlImpl.UpdateProduct(value);

                        myReader = await objCmd.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }

                return new JsonResult("Updated Product Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception UpdateProduct", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("UpdateProduct", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public async Task<ActionResult<ProductModel>> DeleteProduct(int id)
        {
            _logger.LogInfo("DeleteProduct", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText = StringSqlImpl.DeleteProduct(id);

                        myReader = await objCmd.ExecuteReaderAsync(); 
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }

                return new JsonResult("Deleted Product Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception DeleteProduct", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("DeleteProduct", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }
    }
}
