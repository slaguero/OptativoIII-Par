using RepositoryLibrary.Models;
using RepositoryLibrary.Repositories;
using System;
using System.Text.RegularExpressions;

namespace ServiceLibrary.Services
{
    public class FacturaService
    {
        private readonly FacturaRepository _facturaRepository;

        public FacturaService(FacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public void AddFactura(Factura factura)
        {
            ValidateFactura(factura);
            _facturaRepository.Add(factura);
        }

        public void UpdateFactura(Factura factura)
        {
            ValidateFactura(factura);
            _facturaRepository.Update(factura);
        }

        public void DeleteFactura(int id)
        {
            _facturaRepository.Delete(id);
        }

        public Factura GetFacturaById(int id)
        {
            return _facturaRepository.GetById(id);
        }

        public IEnumerable<Factura> ListFacturas()
        {
            return _facturaRepository.GetAll();
        }

        private void ValidateFactura(Factura factura)
        {
            if (!Regex.IsMatch(factura.Nro_Factura, @"^\d{3}-\d{3}-\d{6}$"))
                throw new Exception("El número de factura no tiene un formato válido.");

            if (factura.Total <= 0)
                throw new Exception("El total debe ser un valor positivo.");

            if (factura.Total_iva5 < 0 || factura.Total_iva10 < 0 || factura.Total_iva < 0)
                throw new Exception("Los valores de IVA no pueden ser negativos.");

            if (string.IsNullOrWhiteSpace(factura.Total_letras) || factura.Total_letras.Length < 6)
                throw new Exception("El total en letras es obligatorio y debe tener al menos 6 caracteres.");
        }
    }
}
