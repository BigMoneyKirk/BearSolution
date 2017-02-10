using System;
using System.Data;
using System.Data.SqlClient;

namespace BearSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            string con = GetConnectionString();
            string query = GetQueryString();

            DataSet ds = new DataSet();
            ds = GetDisconnectedResult(con, query);

            // returns the bears' first names
            //ShowReadResult(con, query);

            Console.WriteLine(ds.GetXml());

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
                SqlDataAdapter adapter = new SqlDataAdapter();// query, connection);
                adapter.SelectCommand = new SqlCommand(query,sqlcon);
                adapter.Fill(ds);
                //AddBear(sqlcon).Fill(ds);

                return ds;
            }
        }

        public static SqlDataAdapter AddBear(SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(
                "INSERT INTO Hibernation.Bear VALUES (4, 'Papa Bear', 1000, 1, 3, 1)", connection);

            adapter.SelectCommand = command;
            return adapter;
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
            return "SELECT * FROM BearName_Type_Cave";
        }

    }
}
