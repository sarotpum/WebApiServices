using SharedService.Models.DepartmentModel;
using SharedService.Models.Employees;
using SharedService.Models.Products;

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

        #region ProductsController

        public static string GetProducts()
        {
            return $@"  SELECT * FROM Products";
        }

        public static string AddProduct(ProductModel value)
        {
            return $@"  INSERT INTO Products 
                            (name, description, price)
                        VALUES
                            (
                                '" + value.name + @"'
                                ,'" + value.description + @"'
                                ,'" + value.price + @"'
                            )
                     ";
        }

        public static string UpdateProduct(ProductModel value)
        {
            return $@"  UPDATE Products SET
                            name = '" + value.name + @"'
                            ,description = '" + value.description + @"'
                            ,price = '" + value.price + @"'
                            where id = '" + value.id + @"'
                     ";
        }

        public static string DeleteProduct(int id)
        {
            return $@"  DELETE FROM Products 
                        WHERE id = " + id + @"  
                     ";
        }

        #endregion
    }
}
