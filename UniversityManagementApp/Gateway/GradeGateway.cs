using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Gateway
{
    public class GradeGateway : Gateway
    {
        public static IEnumerable<Grade> GetGrades()
        {
            List<Grade> grades = null;
            string sql = "SELECT * FROM Grades";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                grades = new List<Grade>();
                while (reader.Read())
                {
                    Grade grade = new Grade
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                    };

                    grades.Add(grade);
                }
            }

            Connection.Close();

            return grades;
        }
    }
}