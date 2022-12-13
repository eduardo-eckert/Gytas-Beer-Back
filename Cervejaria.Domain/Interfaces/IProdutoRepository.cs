using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervejaria.Domain.repositories
{
    public interface IProdutoRepository
    {
        void CadastrarProduto(Produto novoProduto);
        bool VerificarProdutoPorDescricao(string descricaoProduto);
        Produto BuscarProdutoPorId(int idProduto);
        List<Produto> BuscarTodosProdutos();
        List<Produto> BuscarTodosProdutosAtivos();
        void EditarProduto(Produto produtoEditado);
        void GerenciarEstoque(Produto produtoGerenciado);
        void AtivarEDesativarProduto(Produto produtoDesativado);
    }
}