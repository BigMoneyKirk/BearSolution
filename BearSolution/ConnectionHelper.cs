using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearSolution
{
    class ConnectionHelper
    {
        public static void OpenSqlConnection()
        {
            // grab the string from GetConnectionString() private method
            string connectionString = GetConnectionString();

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                Console.WriteLine($"state: {connection.State}");
                Console.WriteLine($"ConnectionString: {connection.ConnectionString}");
            }
        }

        private static string GetConnectionString()
        {
            return "Data Source=myinstancedemo.chppvnuzl4vk.us-east-1.rds.amazonaws.com,1433;Initial Catalog=BearDB;Persist Security Info=True;User ID=stephenkirkland;Password=12345678;Encrypt=False;";
        }

    }
}
