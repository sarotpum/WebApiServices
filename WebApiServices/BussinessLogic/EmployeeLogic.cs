using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedService.Contracts;
using SharedService.Implements;
using SharedService.LogProvider.Interface;
using SharedService.Models;
using SharedService.Models.Employees;
using System.Data;
using System.Data.SqlClient;

namespace WebApiServices.BussinessLogic
{
    public class EmployeeLogic
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILoggerService _logger;
        private readonly IWebHostEnvironment _env;
        private readonly string _connectString;

        public EmployeeLogic(ILoggerService logger, IOptions<AppSettings> options, IWebHostEnvironment env)
        {
            _logger = logger;
            _options = options;
            _connectString = _options.Value.databaseSetting.ConnectionString;
            _env = env;
        }

        public JsonResult GetEmployee()
        {
            _logger.LogInfo("GetEmployee", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText = StringSqlImpl.GetEmployee();

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
                _logger.LogInfo("GetEmployee", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public async Task<List<EmployeeModel>> GetEmployee2()
        {
            _logger.LogInfo("GetEmployee2", Constant.IN, typeof(DepartmentLogic).Name);

            List<EmployeeModel> result = new List<EmployeeModel>();
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
                        objCmd.CommandText = StringSqlImpl.GetEmployee();

                        myReader = await objCmd.ExecuteReaderAsync();

                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                result.Add(new EmployeeModel()
                                {
                                    EmployeeId = myReader.GetInt32(0),
                                    EmployeeName = myReader.GetString(1),
                                    Department = myReader.GetString(2),
                                    DateOfJoining = myReader.GetDateTime(3),
                                    PhotoFileName = myReader.GetString(4)
                                });
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception GetEmployee2", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return result;
            }
            finally
            {
                _logger.LogInfo("GetEmployee2", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult AddEmployee(EmployeeModel value)
        {
            _logger.LogInfo("AddEmployee", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText = StringSqlImpl.AddEmployee(value);

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
                _logger.LogError("ERROR!!! Exception AddEmployee", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("AddEmployee", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult UpdateEmployee(EmployeeModel value)
        {
            _logger.LogInfo("UpdateEmployee", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText= StringSqlImpl.UpdateEmployee(value);

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
                _logger.LogError("ERROR!!! Exception UpdateEmployee", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("UpdateEmployee", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult DeleteEmployee(int id)
        { 
            _logger.LogInfo("DeleteEmployee", Constant.IN, typeof(DepartmentLogic).Name);

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
                        objCmd.CommandText = StringSqlImpl.DeleteEmployee(id);

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
                _logger.LogError("ERROR!!! Exception DeleteEmployee", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("DeleteEmployee", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult SaveFileEmployee(HttpRequest request)
        {
            _logger.LogInfo("SaveFileEmployee", Constant.IN, typeof(DepartmentLogic).Name);

            try
            {
                var httpRequest = request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR!!! Exception SaveFileEmployee", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult("anonymous.png");
            }
            finally
            {
                _logger.LogInfo("SaveFileEmployee", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }

        public JsonResult GetAllDepartmentNames()
        {
            _logger.LogInfo("GetAllDepartmentNames", Constant.IN, typeof(DepartmentLogic).Name);

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
                _logger.LogError("ERROR!!! Exception GetAllDepartmentNames", ex.Message + " : " + ex.StackTrace, typeof(DepartmentLogic).Name);
                return new JsonResult(table);
            }
            finally
            {
                _logger.LogInfo("GetAllDepartmentNames", Constant.OUT, typeof(DepartmentLogic).Name);
            }
        }
    }
}
