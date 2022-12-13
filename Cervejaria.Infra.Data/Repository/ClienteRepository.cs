using Cervejaria.Domain;
using Cervejaria.Domain.Exceptions;
using Cervejaria.Domain.repositories;
using Cervejaria.Infra.Data.DAO;
using System;
using System.Collections.Generic;

namespace Cervejaria.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDAO _clienteDAO;

        public ClienteRepository()
        {
            _clienteDAO = new ClienteDAO();
        }

        public void CadastrarCliente(Cliente novoCliente)
        {
            if (Cliente.ValidacaoCliente(novoCliente))
            {
                var clienteBuscado = VerificarClientePorCpf(novoCliente.CpfCliente);
                if (!clienteBuscado)
                    _clienteDAO.CadastrarCliente(novoCliente);
                else
                    throw new AlreadyExists(
                        $"O cliente com CPF {novoCliente.CpfCliente} já está cadastrado no sistema!"
                    );
            }
            else
                throw new InvalidObject($"O objeto de entrada é inválido!");
        }

        public Cliente BuscarClientePorCpf(string cpf)
        {
            return _clienteDAO.BuscarClientePorCpf(cpf);
        }

        public Cliente BuscarClientePorId(int id)
        {
            return _clienteDAO.BuscarClientePorId(id);
        }

        public bool VerificarClientePorCpf(string cpf)
        {
            return _clienteDAO.VerificarClientePorCpf(cpf);
        }

        public List<Cliente> BuscarTodosClientes()
        {
            var listaClientes = _clienteDAO.BuscarTodosClientes();
            if (listaClientes.Count == 0)
                throw new Exception("Não há Clientes registrados no sistema!");
            return listaClientes;
        }

        public void EditarCliente(Cliente clienteEditado)
        {
            if (Cliente.ValidacaoCliente(clienteEditado))
            {
                var clienteBuscado = BuscarClientePorCpf(clienteEditado.CpfCliente);
                if (clienteBuscado == null)
                    throw new Exception($"Cliente não encontrado!");
                else
                    _clienteDAO.EditarCliente(clienteEditado);
            }
            else
                throw new InvalidObject($"O objeto de entrada é inválido!");
        }

        public void ExcluirCliente(string cpfClienteExcluido)
        {
            var clienteBuscado = BuscarClientePorCpf(cpfClienteExcluido);
            if (clienteBuscado == null)
                throw new Exception(
                    $"O cliente com o CPF {cpfClienteExcluido} não foi encontrado!"
                );
            else
                _clienteDAO.ExcluirCliente(clienteBuscado);
        }
    }
}
