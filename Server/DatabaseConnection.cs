using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class DatabaseConnection 
    {
        public void WriteOnDataBase(List<AcessLog> data)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "tcp:., 1433";
                builder.UserID = "SA";
                builder.Password = "p@ssw0rd_sql";
                builder.InitialCatalog = "SquidProject";

                StringBuilder sb = new StringBuilder();
                foreach(AcessLog log in data)
                {
                    sb.Append($"INSERT into Logs (Number,RandomNumber,Ipv6,Protocol,Port, Method, Url, Test, Type) VALUES ({log.Number},{log.RandomNumber},{log.Ipv6},{log.Protocol},{log.Port},{log.Method},{log.Url},{log.Test},{log.Type})");
                }

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand query = new SqlCommand(sb.ToString()))
                    {
                        query.Connection = connection;

                        query.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException err)
            {
                Console.WriteLine(err.ToString());
            }
        }
        public void HandleConnection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "tcp:., 1433";
                builder.UserID = "SA";
                builder.Password = "p@ssw0rd_sql";
                builder.InitialCatalog = "SquidProject";

                Console.WriteLine(builder);

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT *");
                    sb.Append("FROM [test]");
                    //sb.Append("JOIN [SalesLT].[Product] p ");
                    //sb.Append("ON pc.productcategoryid = p.productcategoryid;");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetInt32(1));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();
        }
    }
}
