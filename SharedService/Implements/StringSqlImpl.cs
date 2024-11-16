using SharedService.Models.DepartmentModel;

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
    }
}
