using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementApp.JsonModels;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Gateway
{
    public class TeacherGateway : Gateway
    {
        public static int Save(Teacher teacher)
        {
            string sql = "INSERT INTO Teachers (Name, Address, Email, ContactNo, DesignationId, DepartmentId, CreditToTake) " +
                        "VALUES (@name, @address, @email, @contactNo, @designationId, @departmentId, @creditToTake)";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("name", teacher.Name);
            Command.Parameters.AddWithValue("address", teacher.Address);
            Command.Parameters.AddWithValue("email", teacher.Email);
            Command.Parameters.AddWithValue("contactNo", teacher.ContactNo);
            Command.Parameters.AddWithValue("designationId", teacher.DesignationId);
            Command.Parameters.AddWithValue("departmentId", teacher.DepartmentId);
            Command.Parameters.AddWithValue("creditToTake", teacher.CreditToTake);

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }

        //All the Teachers
        public static IList<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = null;

            string sql = "SELECT * FROM Teachers";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                teachers = new List<Teacher>();
                while (reader.Read())
                {
                    Teacher teacher = new Teacher
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                    };

                    teachers.Add(teacher);
                }
            }

            Connection.Close();

            return teachers;
        }


        public static IList<Teacher> GetTeachersByDepartmentId(int departmentId)
        {
            List<Teacher> teachers = null;

            string sql = "SELECT * FROM Teachers WHERE DepartmentId = " + departmentId;

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                teachers = new List<Teacher>();
                while (reader.Read())
                {
                    Teacher teacher = new Teacher
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                    };

                    teachers.Add(teacher);
                }
            }

            Connection.Close();

            return teachers;
        }


        public static Teacher GetTeacherById(int teacherId)
        {
            Teacher teacher = null;
            string sql = "SELECT * FROM Teachers WHERE Id = " + teacherId;

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                teacher = new Teacher
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    CreditToTake = Convert.ToDouble(reader["CreditToTake"])
                };
            }

            Connection.Close();

            return teacher;
        }

        public static double GetTakenCredit(int teacherId)
        {
            double total = 0.0;
            string sql = "SELECT SUM(c.Credit) as Total FROM CourseAssignments ca" +
                        " INNER JOIN Courses c ON c.Id = ca.CourseId" +
                        " WHERE ca.TeacherId = " + teacherId;

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                total = Convert.ToDouble(reader["Total"]);
            }

            Connection.Close();

            return total;
        }

        public static TeacherCreditReportJsonModel GetTeacherCreditReport(int teacherId)
        {
            Teacher teacher = GetTeacherById(teacherId);
            if (teacher == null)
                return null;

            TeacherCreditReportJsonModel jsonModel = new TeacherCreditReportJsonModel();
            jsonModel.CreditToTake = teacher.CreditToTake;
            jsonModel.RemainingCredit = teacher.CreditToTake - GetTakenCredit(teacherId);

            return jsonModel;

        }
    }
}