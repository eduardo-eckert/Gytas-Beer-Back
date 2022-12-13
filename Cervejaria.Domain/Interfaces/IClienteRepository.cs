using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervejaria.Domain.repositories
{
    public interface IClienteRepository
    {
        void CadastrarCliente(Cliente novoCliente);
        Cliente BuscarClientePorCpf(string cpf);
        Cliente BuscarClientePorId(int id);
        bool VerificarClientePorCpf(string cpf);
        List<Cliente> BuscarTodosClientes();
        void EditarCliente(Cliente clienteEditado);
        void ExcluirCliente(string cpfClienteExcluido);
    }
}