using System;
using System.Linq;
using RepositoryLibrary;
using RepositoryLibrary.Models;
using RepositoryLibrary.Repositories;
using ServiceLibrary.Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Host=localhost;Port=5432;Database=optativo;Username=postgres;Password=1234";
            var databaseConnection = new DatabaseConnection(connectionString);

            // Inicializar Repositorios y Servicios
            var sucursalRepository = new SucursalRepository(databaseConnection);
            var sucursalService = new SucursalService(sucursalRepository);

            var facturaRepository = new FacturaRepository(databaseConnection);
            var facturaService = new FacturaService(facturaRepository);

            var clienteRepository = new ClienteRepository(databaseConnection);
            var clienteService = new ClienteService(clienteRepository);

            var productoRepository = new ProductoRepository(databaseConnection);
            var productoService = new ProductoService(productoRepository);

            // Ejemplo de uso: Añadir una nueva sucursal
            var nuevaSucursal = new Sucursal
            {
                Descripcion = "Sucursal 1",
                Direccion = "123 Calle Falsa",
                Telefono = "1234567890",
                Whatsapp = "0987654321",
                Mail = "sucursal1@ejemplo.com",
                Estado = "Activo"
            };

            try
            {
                sucursalService.ValidateSucursal(nuevaSucursal);
                sucursalRepository.Add(nuevaSucursal);
                Console.WriteLine("Sucursal añadida exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al añadir sucursal: {ex.Message}");
            }

            // Obtener la sucursal añadida (asume que la última sucursal añadida es la que necesitamos)
            var sucursales = sucursalRepository.GetAll();
            var sucursalRecienAñadida = sucursales.LastOrDefault();

            // Imprimir la sucursal añadida
            if (sucursalRecienAñadida != null)
            {
                Console.WriteLine($"Id: {sucursalRecienAñadida.Id}, Descripción: {sucursalRecienAñadida.Descripcion}, Dirección: {sucursalRecienAñadida.Direccion}, " +
                                  $"Teléfono: {sucursalRecienAñadida.Telefono}, Whatsapp: {sucursalRecienAñadida.Whatsapp}, Email: {sucursalRecienAñadida.Mail}, Estado: {sucursalRecienAñadida.Estado}");
            }

            // Ejemplo de uso: Añadir un nuevo cliente
            var nuevoCliente = new Cliente
            {
                Id_banco = 1, // Asume que el banco con Id 1 existe
                Nombre = "Juan",
                Apellido = "Perez",
                Documento = "123456789",
                Direccion = "456 Calle Real",
                Mail = "juan.perez@ejemplo.com",
                Celular = "0987654321",
                Estado = "Activo"
            };

            try
            {
                clienteService.ValidateCliente(nuevoCliente);
                clienteRepository.Add(nuevoCliente);
                Console.WriteLine("Cliente añadido exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al añadir cliente: {ex.Message}");
            }

            // Obtener el cliente añadido (asume que el último cliente añadido es el que necesitamos)
            var clientes = clienteRepository.GetAll();
            var clienteRecienAñadido = clientes.LastOrDefault();

            // Imprimir el cliente añadido
            if (clienteRecienAñadido != null)
            {
                Console.WriteLine($"Id: {clienteRecienAñadido.Id}, Nombre: {clienteRecienAñadido.Nombre}, Apellido: {clienteRecienAñadido.Apellido}, " +
                                  $"Documento: {clienteRecienAñadido.Documento}, Dirección: {clienteRecienAñadido.Direccion}, " +
                                  $"Email: {clienteRecienAñadido.Mail}, Celular: {clienteRecienAñadido.Celular}, Estado: {clienteRecienAñadido.Estado}");
            }

            // Ejemplo de uso: Añadir un nuevo producto
            var nuevoProducto = new Producto
            {
                Descripcion = "Producto A",
                Cantidad_minima = 2,
                Cantidad_stock = 10,
                Precio_compra = 100m,
                Precio_venta = 150m,
                Categoria = "Categoría A",
                Marca = "Marca A",
                Estado = "Activo"
            };

            try
            {
                productoService.ValidateProducto(nuevoProducto);
                productoRepository.Add(nuevoProducto);
                Console.WriteLine("Producto añadido exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al añadir producto: {ex.Message}");
            }

            // Obtener el producto añadido (asume que el último producto añadido es el que necesitamos)
            var productos = productoRepository.GetAll();
            var productoRecienAñadido = productos.LastOrDefault();

            // Imprimir el producto añadido
            if (productoRecienAñadido != null)
            {
                Console.WriteLine($"Id: {productoRecienAñadido.Id}, Descripción: {productoRecienAñadido.Descripcion}, Cantidad mínima: {productoRecienAñadido.Cantidad_minima}, " +
                                  $"Cantidad en stock: {productoRecienAñadido.Cantidad_stock}, Precio de compra: {productoRecienAñadido.Precio_compra}, " +
                                  $"Precio de venta: {productoRecienAñadido.Precio_venta}, Categoría: {productoRecienAñadido.Categoria}, Marca: {productoRecienAñadido.Marca}, Estado: {productoRecienAñadido.Estado}");
            }

            // Ejemplo de uso: Añadir una nueva factura
            var nuevaFactura = new Factura
            {
                Id_cliente = clienteRecienAñadido.Id,  // Usar el ID del cliente recién añadido
                Id_sucursal = sucursalRecienAñadida.Id, // Usar el ID de la sucursal recién añadida
                Nro_Factura = "123-456-789012",
                Fecha_Hora = DateTime.Now,
                Total = 1500.50m,
                Total_iva5 = 50.00m,
                Total_iva10 = 100.00m,
                Total_iva = 150.00m,
                Total_letras = "Mil quinientos con cincuenta"
            };

            try
            {
                facturaService.ValidateFactura(nuevaFactura);
                facturaRepository.Add(nuevaFactura);
                Console.WriteLine("Factura añadida exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al añadir factura: {ex.Message}");
            }

            // Obtener la factura añadida (asume que la última factura añadida es la que necesitamos)
            var facturas = facturaRepository.GetAll();
            var facturaRecienAñadida = facturas.LastOrDefault();

            // Imprimir la factura añadida
            if (facturaRecienAñadida != null)
            {
                Console.WriteLine($"Id: {facturaRecienAñadida.Id}, Cliente: {facturaRecienAñadida.Id_cliente}, Sucursal: {facturaRecienAñadida.Id_sucursal}, " +
                                  $"Número de Factura: {facturaRecienAñadida.Nro_Factura}, Fecha: {facturaRecienAñadida.Fecha_Hora}, Total: {facturaRecienAñadida.Total}, " +
                                  $"IVA 5%: {facturaRecienAñadida.Total_iva5}, IVA 10%: {facturaRecienAñadida.Total_iva10}, Total IVA: {facturaRecienAñadida.Total_iva}, " +
                                  $"Total en Letras: {facturaRecienAñadida.Total_letras}");
            }

            // Ejemplo de uso: Listar todas las facturas
            try
            {
                facturas = facturaService.ListFacturas();
                Console.WriteLine("Listado de facturas:");
                foreach (var factura in facturas)
                {
                    Console.WriteLine($"Id: {factura.Id}, Cliente: {factura.Id_cliente}, Sucursal: {factura.Id_sucursal}, " +
                                      $"Número de Factura: {factura.Nro_Factura}, Fecha: {factura.Fecha_Hora}, Total: {factura.Total}, " +
                                      $"IVA 5%: {factura.Total_iva5}, IVA 10%: {factura.Total_iva10}, Total IVA: {factura.Total_iva}, " +
                                      $"Total en Letras: {factura.Total_letras}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar facturas: {ex.Message}");
            }

            // Ejemplo de uso: Añadir detalles de factura
            var nuevoDetalleFactura = new DetalleFactura
            {
                Id_Factura = facturaRecienAñadida.Id,  // Usar el ID de la factura recién añadida
                Id_Producto = productoRecienAñadido.Id, // Usar el ID del producto recién añadido
                Cantidad_producto = 5,
                Subtotal = 750m
            };

            try
            {
                facturaRepository.AddDetalleFactura(nuevoDetalleFactura);
                Console.WriteLine("Detalle de factura añadido exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al añadir detalle de factura: {ex.Message}");
            }

            // Listar detalles de la factura añadida
            try
            {
                var detallesFactura = facturaService.ListarDetalle(facturaRecienAñadida.Id);
                Console.WriteLine("Detalles de la factura:");
                foreach (var detalle in detallesFactura)
                {
                    Console.WriteLine($"Id: {detalle.Id}, Id Factura: {detalle.Id_Factura}, Id Producto: {detalle.Id_Producto}, " +
                                      $"Cantidad Producto: {detalle.Cantidad_producto}, Subtotal: {detalle.Subtotal}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar detalles de factura: {ex.Message}");
            }
        }
    }
}
