using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Gateway
{
    public class DayGateway : Gateway
    {
        //All the days
        public static IEnumerable<Day> GetAllDays()
        {
            List<Day> days = null;

            string sql = "SELECT * FROM DaysOfWeek";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                days = new List<Day>();
                while (reader.Read())
                {
                    Day day = new Day
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                    };

                    days.Add(day);
                }
            }

            Connection.Close();

            return days;
        }
    }
}