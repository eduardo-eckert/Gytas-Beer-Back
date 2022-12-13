using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervejaria.Domain.repositories
{
    public interface IPedidoRepository
    {
        void RealizarPedido(Pedido pedidoRealizado);
        void AtualizarStatus(int idPedido, Status statusPedido);
        Pedido BuscarPedidoPorId(int idPedido);
        List<Pedido> BuscarTodosPedidos();
        void ExcluirPedido(int pedidoExcluido);
    }
}
