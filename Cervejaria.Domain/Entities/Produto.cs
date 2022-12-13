using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervejaria.Domain
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QtdEstoque { get; set; }
        public DateTime DataValidade { get; set; }
        public bool Ativo { get; set; }

        public Produto() { }

        public Produto(
            int idProduto,
            string descricao,
            decimal preco,
            int qtdEstoque,
            DateTime dataValidade
        )
        {
            IdProduto = idProduto;
            Descricao = descricao;
            Preco = preco;
            QtdEstoque = qtdEstoque;
            DataValidade = dataValidade;
        }

        public bool ValidacaoProduto(Produto produto)
        {
            return (produto.Preco > 0 && produto.Descricao.Length > 3)
                && produto.DataValidade > DateTime.Now;
        }

        public void GerenciarEstoque(int valorEmEstoque, int valorAdicionado)
        {
            QtdEstoque = valorEmEstoque + valorAdicionado;
        }

        public void ReduzirEstoque(int qtdEmEstoque, int qtdPedido)
        {
            QtdEstoque = qtdEmEstoque - qtdPedido;
        }

        public void AtivarEDesativarProduto()
        {
            Ativo = !Ativo;
        }
    }
}
