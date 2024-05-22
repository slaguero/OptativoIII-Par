using RepositoryLibrary.Models;
using RepositoryLibrary.Repositories;
using System;
using System.Text.RegularExpressions;

namespace ServiceLibrary.Services
{
    public class SucursalService
    {
        private readonly SucursalRepository _sucursalRepository;

        public SucursalService(SucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public void AddSucursal(Sucursal sucursal)
        {
            ValidateSucursal(sucursal);
            _sucursalRepository.Add(sucursal);
        }

        public void UpdateSucursal(Sucursal sucursal)
        {
            ValidateSucursal(sucursal);
            _sucursalRepository.Update(sucursal);
        }

        public void DeleteSucursal(int id)
        {
            _sucursalRepository.Delete(id);
        }

        public Sucursal GetSucursalById(int id)
        {
            return _sucursalRepository.GetById(id);
        }

        public IEnumerable<Sucursal> ListSucursales()
        {
            return _sucursalRepository.GetAll();
        }

        private void ValidateSucursal(Sucursal sucursal)
        {
            if (sucursal.Direccion.Length < 10)
                throw new Exception("La dirección debe tener al menos 10 caracteres.");

            if (!IsValidEmail(sucursal.Mail))
                throw new Exception("El email no es válido.");

            if (string.IsNullOrWhiteSpace(sucursal.Descripcion))
                throw new Exception("La descripción es obligatoria.");

            if (string.IsNullOrWhiteSpace(sucursal.Estado))
                throw new Exception("El estado es obligatorio.");
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
