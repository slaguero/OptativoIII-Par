using System;
using System.Text.RegularExpressions;
using RepositoryLibrary.Models;
using RepositoryLibrary.Repositories;

namespace ServiceLibrary.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public void ValidateCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || cliente.Nombre.Length < 3)
            {
                throw new ArgumentException("El nombre es obligatorio y debe tener al menos 3 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(cliente.Apellido) || cliente.Apellido.Length < 3)
            {
                throw new ArgumentException("El apellido es obligatorio y debe tener al menos 3 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(cliente.Documento) || cliente.Documento.Length < 3)
            {
                throw new ArgumentException("La cédula es obligatoria y debe tener al menos 3 caracteres.");
            }

            if (!Regex.IsMatch(cliente.Celular, @"^\d{10}$"))
            {
                throw new ArgumentException("El celular debe ser un número de 10 dígitos.");
            }
        }

        public void AddCliente(Cliente cliente)
        {
            ValidateCliente(cliente);
            _clienteRepository.Add(cliente);
        }

        public IEnumerable<Cliente> ListClientes()
        {
            return _clienteRepository.GetAll();
        }
    }
}
