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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidoDAO _pedidoDAO;
        private readonly ProdutoDAO _produtoDAO;
        private readonly ClienteDAO _clienteDAO;

        public PedidoRepository()
        {
            _pedidoDAO = new PedidoDAO();
            _produtoDAO = new ProdutoDAO();
            _clienteDAO = new ClienteDAO();
        }

        public void RealizarPedido(Pedido novoPedido)
        {
            novoPedido.ProdutoId = novoPedido.Produto.IdProduto;
            if (novoPedido.ValidacaoPedido(novoPedido))
            {
                novoPedido.CalcularValorTotal(novoPedido.Produto.Preco);
                novoPedido.Cliente = _clienteDAO.BuscarClientePorCpf(novoPedido.CpfCliente);
                novoPedido.Produto.ReduzirEstoque(
                    novoPedido.Produto.QtdEstoque,
                    novoPedido.QtdProduto
                );
                _produtoDAO.GerenciarEstoque(novoPedido.Produto);
                if (novoPedido.Cliente != null)
                {
                    if (Cliente.ValidacaoCliente(novoPedido.Cliente))
                    {
                        novoPedido.Cliente.CalcularPontos(novoPedido);
                        _clienteDAO.AtualizarPontos(novoPedido.Cliente);
                    }
                }
                _pedidoDAO.RealizarPedido(novoPedido);
            }
            else
                throw new InvalidObject($"A quantidade inserida não é válida!");
        }

        public List<Pedido> BuscarTodosPedidos()
        {
            var listaPedidos = _pedidoDAO.BuscarTodosPedidos();
            if (listaPedidos.Count == 0)
                throw new Exception("Não há pedidos registrados no sistema!");
            return listaPedidos;
        }

        public Pedido BuscarPedidoPorId(int idPedido)
        {
            return _pedidoDAO.BuscarPedidoPorId(idPedido);
        }

        public void AtualizarStatus(int idPedido, Status statusPedido)
        {
            var pedidoBuscado = BuscarPedidoPorId(idPedido);

            if (pedidoBuscado.Status == (Status)2)
                throw new Forbidden(
                    $"Não é possível alterar o status, pois o pedido já foi concluído"
                );
            else
            {
                pedidoBuscado.Status = statusPedido;
                _pedidoDAO.AtualizarStatus(pedidoBuscado);
            }
        }

        public void ExcluirPedido(int idPedido)
        {
            var pedidoBuscado = BuscarPedidoPorId(idPedido);
            if (pedidoBuscado == null)
                throw new Exception($"O pedido com id {idPedido} não foi encontrado!");
            if (pedidoBuscado.Status == (Status)2)
                throw new Forbidden($"A exclusão não é permitida, pois o pedido já foi concluído");
            else
                _pedidoDAO.ExcluirPedido(pedidoBuscado);
        }
    }
}
