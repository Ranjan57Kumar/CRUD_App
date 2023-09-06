using System.Data;
using System.Data.SqlClient;
using CRUD_App.Models;

namespace CRUD_App.Dal  
{
    public class DAL
    {
        public Response GetAllEmployees(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT E.EmployeeId,E.EmployeeName,E.DepartmentId,D.DepartmentName FROM Employees E INNER JOIN Departments D ON E.DepartmentId = D.DepartmentId", connection);
            DataTable dt = new DataTable();
            List<Employee> lstemployees = new List<Employee>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(dt.Rows[i]["EmployeeId"]);
                    emp.EmployeeName = Convert.ToString(dt.Rows[i]["EmployeeName"]);
                    emp.DepartmentId = Convert.ToInt32(dt.Rows[i]["DepartmentId"]);
                    emp.DepartmentName = Convert.ToString(dt.Rows[i]["DepartmentName"]);
                    lstemployees.Add(emp);
                }
            }
            if (lstemployees.Count > 0)
            {
                response.StatusMessage = "Data Found";
                response.ListEmployees = lstemployees;
            }
            else
            {
                response.StatusMessage = "No Data Found";
                response.ListEmployees = null;
            }
            return response;
        }

        public Response GetEmployeeById(SqlConnection connection, int Id)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT E.EmployeeId,E.EmployeeName,E.DepartmentId,D.DepartmentName FROM Employees E INNER JOIN Departments D ON E.DepartmentId = D.DepartmentId where E.EmployeeId = " + Id + "", connection);
            DataTable dt = new DataTable();
            Employee employee = new Employee();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Employee emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]);
                emp.EmployeeName = Convert.ToString(dt.Rows[0]["EmployeeName"]);
                emp.DepartmentId = Convert.ToInt32(dt.Rows[0]["DepartmentId"]);
                emp.DepartmentName = Convert.ToString(dt.Rows[0]["DepartmentName"]);
                response.StatusMessage = "Data Found";
                response.Employee = emp;

            }
            else
            {
                response.StatusMessage = "No Data Found";
                response.Employee = null;
            }
            return response;
        }

        public Response AddEmployee(SqlConnection connection, Employee emp)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO Employees (EmployeeName, DepartmentId) VALUES ('" + emp.EmployeeName + "', " + emp.DepartmentId + ")", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusMessage = "Employee Added.";
            }
            else
            {
                response.StatusMessage = "No Data Inserted";
            }
            return response;
        }

        public Response UpdateEmployee(SqlConnection connection, Employee emp)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("UPDATE Employees SET EmployeeName = '" + emp.EmployeeName + "', DepartmentId = " + emp.DepartmentId + " WHERE EmployeeId = " + emp.EmployeeId + "", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusMessage = "Employee Updated.";
            }
            else
            {
                response.StatusMessage = "No Data Updated";
            }
            return response;
        }

        public Response DeleteEmployee(SqlConnection connection, int Id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeId = " + Id + "", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusMessage = "Employee Deleted.";
            }
            else
            {
                response.StatusMessage = "No Data Deleted";
            }
            return response;
        }

        public Response GetAllDepartments(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Departments", connection);
            DataTable dt = new DataTable();
            List<Department> lstDepts = new List<Department>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Department dept = new Department();
                    dept.DepartmentId = Convert.ToInt32(dt.Rows[i]["DepartmentId"]);
                    dept.DepartmentName = Convert.ToString(dt.Rows[i]["DepartmentName"]); ;
                    lstDepts.Add(dept);
                }
            }
            if (lstDepts.Count > 0)
            {
                response.StatusMessage = "Data Found";
                response.ListDepartments = lstDepts;
            }
            else
            {
                response.StatusMessage = "No Data Found";
                response.ListEmployees = null;
            }
            return response;
        }

        public Response AddDepartment(SqlConnection connection, Department dept)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO Departments (DepartmentName) VALUES ('" + dept.DepartmentName + "')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusMessage = "Department Added.";
            }
            else
            {
                response.StatusMessage = "No Data Inserted";
            }
            return response;
        }

        public Response UpdateDepartment(SqlConnection connection, Department dept)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("UPDATE Departments SET DepartmentName = '" + dept.DepartmentName + "' WHERE DepartmentId = " + dept.DepartmentId + "", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusMessage = "Department Updated.";
            }
            else
            {
                response.StatusMessage = "No Data Updated";
            }
            return response;
        }

        public Response DeleteDepartment(SqlConnection connection, int Id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("DELETE FROM Departments WHERE DepartmentId = " + Id + "", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusMessage = "Department Deleted.";
            }
            else
            {
                response.StatusMessage = "No Data Deleted";
            }
            return response;
        }

        public Response GetAllEmployeesByDepartmentId(SqlConnection connection, int departmentId)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT E.EmployeeId,E.EmployeeName,E.DepartmentId,D.DepartmentName FROM Employees E INNER JOIN Departments D ON E.DepartmentId = D.DepartmentId where D.DepartmentId = " + departmentId + "", connection);
            DataTable dt = new DataTable();
            List<Employee> lstemployees = new List<Employee>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(dt.Rows[i]["EmployeeId"]);
                    emp.EmployeeName = Convert.ToString(dt.Rows[i]["EmployeeName"]);
                    emp.DepartmentId = Convert.ToInt32(dt.Rows[i]["DepartmentId"]);
                    emp.DepartmentName = Convert.ToString(dt.Rows[i]["DepartmentName"]);
                    lstemployees.Add(emp);
                }
            }
            if (lstemployees.Count > 0)
            {
                response.StatusMessage = "Data Found";
                response.ListEmployees = lstemployees;
            }
            else
            {
                response.StatusMessage = "No Data Found";
                response.ListEmployees = null;
            }
            return response;

        }
    }
}
