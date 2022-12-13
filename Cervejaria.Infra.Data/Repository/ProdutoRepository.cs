using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cervejaria.Domain;
using Cervejaria.Domain.Exceptions;
using Cervejaria.Domain.repositories;
using Cervejaria.Infra.Data.DAO;

namespace Cervejaria.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDAO _produtoDAO;

        public ProdutoRepository()
        {
            _produtoDAO = new ProdutoDAO();
        }

        public void CadastrarProduto(Produto novoProduto)
        {
            if (novoProduto.ValidacaoProduto(novoProduto))
            {
                var produtoBuscado = VerificarProdutoPorDescricao(novoProduto.Descricao);
                if (!produtoBuscado)
                    _produtoDAO.CadastrarProduto(novoProduto);
                else
                    throw new AlreadyExists(
                        $"O produto {novoProduto.Descricao} já está cadastrado no sistema!"
                    );
            }
            else
                throw new InvalidObject($"O objeto de entrada é inválido!");
        }

        public Produto BuscarProdutoPorId(int idProduto)
        {
            return _produtoDAO.BuscarProdutoPorId(idProduto);
        }

        public bool VerificarProdutoPorDescricao(string descricaoProduto)
        {
            return _produtoDAO.VerificarProdutoPorDescricao(descricaoProduto);
        }

        public List<Produto> BuscarTodosProdutos()
        {
            var listaProdutos = _produtoDAO.BuscarTodosProdutos();
            if (listaProdutos.Count == 0)
                throw new Exception("Não há produtos registrados no sistema!");
            return listaProdutos;
        }

        public List<Produto> BuscarTodosProdutosAtivos()
        {
            var listaProdutosAtivos = _produtoDAO.BuscarTodosProdutosAtivos();
            if (listaProdutosAtivos.Count == 0)
                throw new Exception("Não há produtos ativos registrados no sistema!");
            return listaProdutosAtivos;
        }

        public void EditarProduto(Produto produtoEditado)
        {
            if (produtoEditado.ValidacaoProduto(produtoEditado))
            {
                var produtoBuscado = BuscarProdutoPorId(produtoEditado.IdProduto);
                if (produtoBuscado == null)
                    throw new Exception($"Produto não encontrado!");
                else
                    _produtoDAO.EditarProduto(produtoEditado);
            }
            else
                throw new InvalidObject($"O objeto de entrada é inválido!");
        }

        public void GerenciarEstoque(Produto produtoGerenciado)
        {
            var produtoBuscado = BuscarProdutoPorId(produtoGerenciado.IdProduto);
            if (produtoBuscado == null)
                throw new Exception($"Produto não encontrado!");
            else
            {
                produtoBuscado.GerenciarEstoque(
                    produtoBuscado.QtdEstoque,
                    produtoGerenciado.QtdEstoque
                );
                _produtoDAO.GerenciarEstoque(produtoBuscado);
            }
        }

        public void AtivarEDesativarProduto(Produto produtoAtivadoDesativado)
        {
            produtoAtivadoDesativado.AtivarEDesativarProduto();
            _produtoDAO.AtivarEDesativarProduto(produtoAtivadoDesativado);
        }
    }
}
