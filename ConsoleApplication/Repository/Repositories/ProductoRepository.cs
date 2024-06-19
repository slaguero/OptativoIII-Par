using Dapper;
using RepositoryLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepositoryLibrary.Repositories
{
    public class ProductoRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public ProductoRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<Producto> GetAll()
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Productos";
                return dbConnection.Query<Producto>(query).ToList();
            }
        }

        public Producto GetById(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Productos WHERE Id = @Id";
                return dbConnection.QueryFirstOrDefault<Producto>(query, new { Id = id });
            }
        }

        public void Add(Producto producto)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    INSERT INTO Productos 
                    (Descripcion, Cantidad_minima, Cantidad_stock, Precio_compra, Precio_venta, Categoria, Marca, Estado) 
                    VALUES 
                    (@Descripcion, @Cantidad_minima, @Cantidad_stock, @Precio_compra, @Precio_venta, @Categoria, @Marca, @Estado)";
                dbConnection.Execute(query, producto);
            }
        }

        public void Update(Producto producto)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = @"
                    UPDATE Productos SET 
                    Descripcion = @Descripcion, 
                    Cantidad_minima = @Cantidad_minima, 
                    Cantidad_stock = @Cantidad_stock, 
                    Precio_compra = @Precio_compra, 
                    Precio_venta = @Precio_venta, 
                    Categoria = @Categoria, 
                    Marca = @Marca, 
                    Estado = @Estado 
                    WHERE Id = @Id";
                dbConnection.Execute(query, producto);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM Productos WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }
    }
}
