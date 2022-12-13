using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervejaria.Domain
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string CpfCliente { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal PtsFidelidade { get; set; }

        public Cliente() { }

        public Cliente(int idCliente, string nome, string cpfCliente, DateTime dataNascimento)
        {
            this.IdCliente = idCliente;
            this.Nome = nome;
            this.CpfCliente = cpfCliente;
            this.DataNascimento = dataNascimento;
        }

        public static bool ValidacaoCliente(Cliente cliente)
        {
            return cliente.CpfCliente.Length == 11
                && cliente.Nome.Length > 3
                && cliente.DataNascimento < DateTime.Now;
        }

        public void CalcularPontos(Pedido pedido)
        {
            PtsFidelidade += pedido.ValorTotal * 2;
        }
    }
}
