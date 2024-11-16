using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedService.Contracts;
using SharedService.Implements;
using SharedService.LogProvider.Interface;
using SharedService.Models;
using SharedService.Models.DepartmentModel;
using System.Data;
using System.Data.SqlClient;

namespace WebApiServices.BussinessLogic
{
    public class DepartmentLogic
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILoggerService _logger;
        private readonly string _connectString;

        public DepartmentLogic(ILoggerService logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _options = options;
            _connectString = _options.Value.databaseSetting.ConnectionString;
        }

        public JsonResult GetDepartment()
        {
            _logger.LogInfo("GetDepartment", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText = StringSqlImpl.GetDepartment();
                         
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
                _logger.LogError("ERROR!!! Exception GetDepartment", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("GetDepartment", Constant.OUT, typeof(DepartmentLogic).Name);
            } 
        }

        public async Task<List<DepartmentModel>> GetDepartment2()
        {
            _logger.LogInfo("GetDepartment2", Constant.IN, typeof(DepartmentLogic).Name);

            List<DepartmentModel> result = new List<DepartmentModel>();
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
                        objCmd.CommandText = StringSqlImpl.GetDepartment();

                        myReader = await objCmd.ExecuteReaderAsync();
                        
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                result.Add(new DepartmentModel()
                                {
                                    DepartmentID = myReader.GetInt32(0),
                                    DepartmentName = myReader.GetString(1)
                                });
                            }
                        } 
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception GetDepartment2", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return result;
            }
            finally
            {
                _logger.LogInfo("GetDepartment2", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult AddDepartment(DepartmentModel value)
        {
            _logger.LogInfo("AddDepartment", Constant.IN, typeof(DepartmentLogic).Name);
            
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
                        objCmd.CommandText = StringSqlImpl.AddDepartment(value);

                        myReader = objCmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }

                return new JsonResult("Added Syccessfully");
            }
            catch (Exception ex)
            { 
                _logger.LogError("ERROR!!! Exception AddDepartment", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult("Error Added Department");
            }
            finally
            {
                _logger.LogInfo("AddDepartment", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult UpdateDepartment(DepartmentModel value)
        {
            _logger.LogInfo("UpdateDepartment", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText = StringSqlImpl.UpdateDepartment(value);

                        myReader = objCmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }

                return new JsonResult("Updated Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception UpdateDepartment", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult("Error Updated Department");
            }
            finally
            {
                _logger.LogInfo("UpdateDepartment", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult DeleteDepartment(int id)
        {
            _logger.LogInfo("DeleteDepartment", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText = StringSqlImpl.DeleteDepartment(id);

                        myReader = objCmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        objConn.Close();
                    }
                }

                return new JsonResult("Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception DeleteDepartment", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult("Error Deleted Department");
            }
            finally
            {
                _logger.LogInfo("DeleteDepartment", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }
    }
}
