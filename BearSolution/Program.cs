using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            //OpenSqlConnection();

            string con = GetConnectionString();
            string query = GetQueryString();

            DataSet ds = GetDisconnectedResult(con, query);

            //Console.WriteLine(ds.GetXml());
            ShowReadResult(con, query);

            /*
             * This is where I started writing my code.
             * Make xml -> dataset, and then query something using LINQ
            */

            Console.ReadLine();
        }

        public static void ShowReadResult(string connection, string query)
        {
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                SqlCommand command = new SqlCommand(query, sqlcon);
                try
                {
                    sqlcon.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    // the rest was set up; this is what we want.

                    while (reader.Read())
                    {
                        // write stuff to the console
                        // look up MSDN documentation for SqlDataReader
                        // print out list of bears
                        Console.WriteLine($"{reader[2]}");
                        //Console.WriteLine($"{reader.GetString(0)}");
                    }
                    reader.Close();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }
        }

        public static DataSet GetDisconnectedResult(string connection, string query)
        {
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query,sqlcon);
                adapter.Fill(ds);

                return ds;
            }
        }

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

        private static string GetQueryString()
        {
            return "SELECT * FROM Hibernation.Bear;"; //+ 
                //"select* from Bear_Caves";
        }

    }
}
