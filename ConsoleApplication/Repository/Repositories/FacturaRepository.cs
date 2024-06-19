using Dapper;
using RepositoryLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepositoryLibrary.Repositories
{
    public class FacturaRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public FacturaRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<Factura> GetAll()
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Factura";
                return dbConnection.Query<Factura>(query).ToList();
            }
        }

        public Factura GetById(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Factura WHERE Id = @Id";
                return dbConnection.QueryFirstOrDefault<Factura>(query, new { Id = id });
            }
        }

        public void Add(Factura factura)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    INSERT INTO Factura 
                    (Id_cliente, Id_sucursal, Nro_Factura, Fecha_Hora, Total, Total_iva5, Total_iva10, Total_iva, Total_letras) 
                    VALUES 
                    (@Id_cliente, @Id_sucursal, @Nro_Factura, @Fecha_Hora, @Total, @Total_iva5, @Total_iva10, @Total_iva, @Total_letras)";
                dbConnection.Execute(query, factura);
            }
        }

        public void Update(Factura factura)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    UPDATE Factura SET 
                    Id_cliente = @Id_cliente, 
                    Id_sucursal = @Id_sucursal, 
                    Nro_Factura = @Nro_Factura, 
                    Fecha_Hora = @Fecha_Hora, 
                    Total = @Total, 
                    Total_iva5 = @Total_iva5, 
                    Total_iva10 = @Total_iva10, 
                    Total_iva = @Total_iva, 
                    Total_letras = @Total_letras 
                    WHERE Id = @Id";
                dbConnection.Execute(query, factura);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM Factura WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }

        public void AddDetalleFactura(DetalleFactura detalleFactura)
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

        public IEnumerable<DetalleFactura> GetDetalleByFacturaId(int facturaId)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    SELECT df.*, p.*
                    FROM Detalle_factura df
                    INNER JOIN Productos p ON df.Id_Producto = p.Id
                    WHERE df.Id_Factura = @FacturaId";
                var detalleFactura = dbConnection.Query<DetalleFactura, Producto, DetalleFactura>(
                    query,
                    (detalle, producto) =>
                    {
                        detalle.Producto = producto;
                        return detalle;
                    },
                    new { FacturaId = facturaId },
                    splitOn: "Id"
                ).ToList();
                return detalleFactura;
            }
        }
    }
}
