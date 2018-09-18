using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Gateway
{
    public class DesignationGateway : Gateway
    {
        //All the departments
        public static List<Designation> GetAllDesignations()
        {
            List<Designation> list = null;

            string sql = "SELECT * FROM Designations";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                list = new List<Designation>();
                while (reader.Read())
                {
                    Designation designation = new Designation();
                    designation.Id = (int)reader["Id"];
                    designation.Title = reader["Title"].ToString();
                    list.Add(designation);
                }
            }

            Connection.Close();

            return list;
        }
    }
}