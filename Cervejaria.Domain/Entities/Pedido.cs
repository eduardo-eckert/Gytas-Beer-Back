using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervejaria.Domain
{
    public class Pedido
    {
        public int IdPedido { get; set; }
#nullable enable
        public string CpfCliente { get; set; }
        public Cliente? Cliente { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int QtdProduto { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public Status Status { get; set; }

        public Pedido()
        {
            DataPedido = DateTime.Now;
        }

        public Pedido(
            int idPedido,
            string cpfCliente,
            Cliente cliente,
            int produtoId,
            Produto produto,
            int qtdProduto,
            DateTime dataPedido
        )
        {
            this.IdPedido = idPedido;
            this.CpfCliente = cpfCliente;
            this.Cliente = cliente;
            this.ProdutoId = produtoId;
            this.Produto = produto;
            this.QtdProduto = qtdProduto;
            this.DataPedido = dataPedido;
        }

        public bool ValidacaoPedido(Pedido pedido)
        {
            return (pedido.QtdProduto > 0 && pedido.QtdProduto <= pedido.Produto.QtdEstoque);
        }

        public void CalcularValorTotal(decimal precoProduto)
        {
            ValorTotal = QtdProduto * precoProduto;
        }
    }

    public enum Status
    {
        Andamento = 0,
        Transito = 1,
        Finalizado = 2
    }
}
