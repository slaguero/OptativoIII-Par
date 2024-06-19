using System;
using RepositoryLibrary.Models;
using RepositoryLibrary.Repositories;

namespace ServiceLibrary.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _productoRepository;

        public ProductoService(ProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public void ValidateProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Descripcion))
            {
                throw new ArgumentException("La descripción es obligatoria.");
            }

            if (producto.Cantidad_minima < 1)
            {
                throw new ArgumentException("La cantidad mínima debe ser mayor a 1.");
            }

            if (producto.Cantidad_stock < 0)
            {
                throw new ArgumentException("La cantidad en stock debe ser un número no negativo.");
            }

            if (producto.Precio_compra <= 0)
            {
                throw new ArgumentException("El precio de compra debe ser un valor positivo.");
            }

            if (producto.Precio_venta <= 0)
            {
                throw new ArgumentException("El precio de venta debe ser un valor positivo.");
            }

            if (string.IsNullOrWhiteSpace(producto.Categoria))
            {
                throw new ArgumentException("La categoría es obligatoria.");
            }

            if (string.IsNullOrWhiteSpace(producto.Marca))
            {
                throw new ArgumentException("La marca es obligatoria.");
            }

            if (string.IsNullOrWhiteSpace(producto.Estado))
            {
                throw new ArgumentException("El estado es obligatorio.");
            }
        }

        public void AddProducto(Producto producto)
        {
            ValidateProducto(producto);
            _productoRepository.Add(producto);
        }

        public IEnumerable<Producto> ListProductos()
        {
            return _productoRepository.GetAll();
        }
    }
}
