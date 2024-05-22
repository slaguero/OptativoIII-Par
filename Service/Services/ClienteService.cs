using RepositoryLibrary.Models;
using RepositoryLibrary.Repositories;
using System;
using System.Text.RegularExpressions;

namespace ServiceLibrary.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public void AddCliente(Cliente cliente)
        {
            ValidateCliente(cliente);
            _clienteRepository.Add(cliente);
        }

        public void UpdateCliente(Cliente cliente)
        {
            ValidateCliente(cliente);
            _clienteRepository.Update(cliente);
        }

        public void DeleteCliente(int id)
        {
            _clienteRepository.Delete(id);
        }

        public Cliente GetClienteById(int id)
        {
            return _clienteRepository.GetById(id);
        }

        public IEnumerable<Cliente> ListClientes()
        {
            return _clienteRepository.GetAll();
        }

        private void ValidateCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || cliente.Nombre.Length < 3)
                throw new Exception("El nombre es obligatorio y debe tener al menos 3 caracteres.");

            if (string.IsNullOrWhiteSpace(cliente.Apellido) || cliente.Apellido.Length < 3)
                throw new Exception("El apellido es obligatorio y debe tener al menos 3 caracteres.");

            if (string.IsNullOrWhiteSpace(cliente.Documento))
                throw new Exception("El documento es obligatorio.");

            if (!Regex.IsMatch(cliente.Celular, @"^\d{10}$"))
                throw new Exception("El celular debe ser un número de 10 dígitos.");

            if (string.IsNullOrWhiteSpace(cliente.Mail) || !IsValidEmail(cliente.Mail))
                throw new Exception("El email no es válido.");

            if (string.IsNullOrWhiteSpace(cliente.Direccion))
                throw new Exception("La dirección es obligatoria.");
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
