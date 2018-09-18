using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Gateway
{
    public class DepartmentGateway : Gateway
    {
        public static int Save(Department department)
        {
            string sql = "INSERT INTO Departments (Code, Name) VALUES (@code, @name)";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("code", department.Code);
            Command.Parameters.AddWithValue("name", department.Name);

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }


        //All the departments
        public static IEnumerable<Department> GetAllDepartments()
        {
            List<Department> departments = null;

            string sql = "SELECT * FROM Departments";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                departments = new List<Department>();
                while (reader.Read())
                {
                    Department department = new Department
                    {
                        Id = (int)reader["Id"],
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString()
                    };

                    departments.Add(department);
                }
            }

            Connection.Close();

            return departments;
        }


        //Get Department By Id
        public static Department GetDepartmentById(int departmentId)
        {
            Department department = null;

            string sql = "SELECT * FROM Departments WHERE Id=" + departmentId;

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                department = new Department
                {
                    Id = (int)reader["Id"],
                    Code = reader["Code"].ToString(),
                    Name = reader["Name"].ToString()
                };
            }

            Connection.Close();

            return department;
        }
    }
}