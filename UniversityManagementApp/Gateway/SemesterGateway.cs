using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Gateway
{
    public class SemesterGateway : Gateway
    {
        //All the departments
        public static IList<Semester> GetAllSemseters()
        {
            List<Semester> semesters = null;

            string sql = "SELECT * FROM Semesters";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                semesters = new List<Semester>();
                while (reader.Read())
                {
                    Semester semester = new Semester
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                    };

                    semesters.Add(semester);
                }
            }

            Connection.Close();

            return semesters;
        }
    }
}