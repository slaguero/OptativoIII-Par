using Dapper;
using RepositoryLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepositoryLibrary.Repositories
{
    public class ClienteRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public ClienteRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<Cliente> GetAll()
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Cliente";
                return dbConnection.Query<Cliente>(query).ToList();
            }
        }

        public Cliente GetById(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Cliente WHERE Id = @Id";
                return dbConnection.QueryFirstOrDefault<Cliente>(query, new { Id = id });
            }
        }

        public void Add(Cliente cliente)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    INSERT INTO Cliente 
                    (Id_banco, Nombre, Apellido, Documento, Direccion, Mail, Celular, Estado) 
                    VALUES 
                    (@Id_banco, @Nombre, @Apellido, @Documento, @Direccion, @Mail, @Celular, @Estado)";
                dbConnection.Execute(query, cliente);
            }
        }

        public void Update(Cliente cliente)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    UPDATE Cliente SET 
                    Id_banco = @Id_banco, 
                    Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    Documento = @Documento, 
                    Direccion = @Direccion, 
                    Mail = @Mail, 
                    Celular = @Celular, 
                    Estado = @Estado 
                    WHERE Id = @Id";
                dbConnection.Execute(query, cliente);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM Cliente WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }
    }
}
