﻿// RepositoryLibrary/DatabaseConnection.cs
using Npgsql;
using System.Data;

namespace RepositoryLibrary
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
