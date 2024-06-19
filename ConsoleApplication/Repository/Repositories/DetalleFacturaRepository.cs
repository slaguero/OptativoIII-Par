using Dapper;
using RepositoryLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepositoryLibrary.Repositories
{
    public class DetalleFacturaRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public DetalleFacturaRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<DetalleFactura> GetAll()
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Detalle_factura";
                return dbConnection.Query<DetalleFactura>(query).ToList();
            }
        }

        public DetalleFactura GetById(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Detalle_factura WHERE Id = @Id";
                return dbConnection.QueryFirstOrDefault<DetalleFactura>(query, new { Id = id });
            }
        }

        public void Add(DetalleFactura detalleFactura)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    INSERT INTO Detalle_factura 
                    (Id_Factura, Id_Producto, Cantidad_producto, Subtotal) 
                    VALUES 
                    (@Id_Factura, @Id_Producto, @Cantidad_producto, @Subtotal)";
                dbConnection.Execute(query, detalleFactura);
            }
        }

        public void Update(DetalleFactura detalleFactura)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    UPDATE Detalle_factura SET 
                    Id_Factura = @Id_Factura, 
                    Id_Producto = @Id_Producto, 
                    Cantidad_producto = @Cantidad_producto, 
                    Subtotal = @Subtotal 
                    WHERE Id = @Id";
                dbConnection.Execute(query, detalleFactura);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM Detalle_factura WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }
    }
}
