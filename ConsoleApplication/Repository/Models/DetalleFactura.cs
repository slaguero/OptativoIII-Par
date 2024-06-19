namespace RepositoryLibrary.Models
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int Id_Factura { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad_producto { get; set; }
        public decimal Subtotal { get; set; }
        public Producto Producto { get; set; } // Agregar la referencia al Producto
    }
}
