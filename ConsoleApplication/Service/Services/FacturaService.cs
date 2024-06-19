using System;
using System.Text.RegularExpressions;
using RepositoryLibrary.Models;
using RepositoryLibrary.Repositories;

namespace ServiceLibrary.Services
{
    public class FacturaService
    {
        private readonly FacturaRepository _facturaRepository;

        public FacturaService(FacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public void ValidateFactura(Factura factura)
        {
            if (!Regex.IsMatch(factura.Nro_Factura, @"^\d{3}-\d{3}-\d{6}$"))
            {
                throw new ArgumentException("El número de factura no es válido. Debe seguir el patrón XXX-XXX-XXXXXX.");
            }

            if (factura.Total <= 0)
            {
                throw new ArgumentException("El total debe ser un valor numérico positivo.");
            }

            if (factura.Total_iva5 < 0)
            {
                throw new ArgumentException("El IVA 5% debe ser un valor numérico no negativo.");
            }

            if (factura.Total_iva10 < 0)
            {
                throw new ArgumentException("El IVA 10% debe ser un valor numérico no negativo.");
            }

            if (factura.Total_iva < 0)
            {
                throw new ArgumentException("El IVA total debe ser un valor numérico no negativo.");
            }

            if (string.IsNullOrWhiteSpace(factura.Total_letras) || factura.Total_letras.Length < 6)
            {
                throw new ArgumentException("El total en letras es obligatorio y debe tener al menos 6 caracteres.");
            }
        }

        public void AddFactura(Factura factura)
        {
            ValidateFactura(factura);
            _facturaRepository.Add(factura);
        }

        public IEnumerable<Factura> ListFacturas()
        {
            return _facturaRepository.GetAll();
        }

        public IEnumerable<DetalleFactura> ListarDetalle(int facturaId)
        {
            return _facturaRepository.GetDetalleByFacturaId(facturaId);
        }
    }
}
