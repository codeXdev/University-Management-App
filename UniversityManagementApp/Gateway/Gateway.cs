using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityManagementApp.Gateway
{
    public class Gateway
    {
        private static SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString);

        public static SqlCommand Command { get; set; }

        public static SqlConnection Connection
        {
            get
            {
                return connection;
            }
            private set { }
        }
    }
}