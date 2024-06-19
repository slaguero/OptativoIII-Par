using System;
using System.Text.RegularExpressions;
using RepositoryLibrary.Models;
using RepositoryLibrary.Repositories;

namespace ServiceLibrary.Services
{
    public class SucursalService
    {
        private readonly SucursalRepository _sucursalRepository;

        public SucursalService(SucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public void ValidateSucursal(Sucursal sucursal)
        {
            if (string.IsNullOrWhiteSpace(sucursal.Direccion) || sucursal.Direccion.Length < 10)
            {
                throw new ArgumentException("La dirección debe tener al menos 10 caracteres.");
            }

            if (!IsValidEmail(sucursal.Mail))
            {
                throw new ArgumentException("El correo electrónico no es válido.");
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddSucursal(Sucursal sucursal)
        {
            ValidateSucursal(sucursal);
            _sucursalRepository.Add(sucursal);
        }

        public IEnumerable<Sucursal> ListSucursales()
        {
            return _sucursalRepository.GetAll();
        }
    }
}
