using Dapper;
using Npgsql;
using RepositoryLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepositoryLibrary.Repositories
{
    public class SucursalRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public SucursalRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<Sucursal> GetAll()
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Sucursal";
                return dbConnection.Query<Sucursal>(query).ToList();
            }
        }

        public Sucursal GetById(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Sucursal WHERE Id = @Id";
                return dbConnection.QueryFirstOrDefault<Sucursal>(query, new { Id = id });
            }
        }

        public void Add(Sucursal sucursal)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    INSERT INTO Sucursal (Descripcion, Direccion, Telefono, Whatsapp, Mail, Estado) 
                    VALUES (@Descripcion, @Direccion, @Telefono, @Whatsapp, @Mail, @Estado)";
                dbConnection.Execute(query, sucursal);
            }
        }

        public void Update(Sucursal sucursal)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    UPDATE Sucursal 
                    SET Descripcion = @Descripcion, 
                        Direccion = @Direccion, 
                        Telefono = @Telefono, 
                        Whatsapp = @Whatsapp, 
                        Mail = @Mail, 
                        Estado = @Estado 
                    WHERE Id = @Id";
                dbConnection.Execute(query, sucursal);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM Sucursal WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }
    }
}
