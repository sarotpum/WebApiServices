using SharedService.Models.DepartmentModel;
using SharedService.Models.Employees;

namespace SharedService.Implements
{
    public static class StringSqlImpl
    {
        #region SQL DepartmentController

        public static string GetDepartment()
        {
            return @$"  SELECT 
                            DepartmentID,
                            DepartmentName
                        FROM Department
                     ";
        }

        public static string AddDepartment(DepartmentModel value)
        {
            return @$"  INSERT INTO Department
                            (DepartmentName)
                        VALUES
                            ('" + value.DepartmentName + "') ";
        }

        public static string UpdateDepartment(DepartmentModel value)
        {
            return $@"  UPDATE Department SET
                            DepartmentName = '" + value.DepartmentName + @"' 
                        WHERE DepartmentId = '" + value.DepartmentID + @"'
                     ";
        }

        public static string DeleteDepartment(int id)
        {
            return $@"  DELETE FROM Department 
                        WHERE DepartmentId = '" + id + "' ";
        }

        #endregion

        #region EmployeeController

        public static string GetEmployee()
        {
            return $@"  SELECT
                            EmployeeId,
                            EmployeeName,
                            Department,
                            DateOfJoining,
                            PhotoFileName
                        FROM Employee
                     ";
        }

        public static string AddEmployee(EmployeeModel value)
        {
            return $@"  INSERT INTO Employee
                            (EmployeeName, Department, DateOfJoining, PhotoFileName)
                        VALUES
                            (
                                '" + value.EmployeeName + @"'
                                ,'" + value.Department + @"'
                                ,CONVERT(DATETIME, '" + value.DateOfJoining + @"', 102)
                                ,'" + value.PhotoFileName + @"'
                            )
                     ";
        }

        public static string UpdateEmployee(EmployeeModel value)
        {
            return $@"  UPDATE Employee SET
                            EmployeeName = '" + value.EmployeeName + @"'
                            ,Department = '" + value.Department + @"'
                            ,DateOfJoining = '" + value.DateOfJoining + @"'
                            ,PhotoFileName = '" + value.PhotoFileName + @"'
                            where EmployeeId = '" + value.EmployeeId + @"' 
                     ";
        }

        public static string DeleteEmployee(int id)
        {
            return $@"  DELETE FROM Employee WHERE EmployeeId = '" + id + "' ";
        }

        #endregion
    }
}
