using Npgsql;
using Shaiya.Origin.Common.Logging;
using System;

namespace Shaiya.Origin.Database.Connector
{
    public class DatabaseConnector
    {
        public bool Connect(string database)
        {
            var connection = GetConnection(database);

            // Check the connection is valid
            try
            {
                using (connection = new NpgsqlConnection(connection.ConnectionString))
                {
                    connection.Open();
                    connection.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return false;
            }
        }

        public NpgsqlConnection GetConnection(string database)
        {
            // Create a new connection instance
            var connection = new NpgsqlConnection();

            // The connection string
            string connString = "Host=127.0.0.1;Username=postgres;Password=password;Database=" + database;

            // Set the connection string
            connection.ConnectionString = connString;

            // Connect to the database
            return connection;
        }
    }
}