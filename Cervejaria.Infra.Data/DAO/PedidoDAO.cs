using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cervejaria.Domain;

namespace Cervejaria.Infra.Data.DAO
{
    public class PedidoDAO
    {
        private readonly string _connectionString =
            @"server=.\SQLexpress;initial catalog=CERVEJARIA;integrated security=true;";

        public void RealizarPedido(Pedido novoPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"INSERT PEDIDOS(CPFCLIENTE, PRODUTOID, QTDPRODUTO, DATAPEDIDO, VALORTOTAL) 
                        VALUES (@CPFCLIENTE, @PRODUTOID, @QTDPRODUTO, @DATAPEDIDO, @VALORTOTAL)";

                    ConverterObjetoParaSQL(novoPedido, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Pedido> BuscarTodosPedidos()
        {
            var listaPedidos = new List<Pedido>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM PEDIDOS;";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var pedidoBuscado = ConverterSqlParaObjeto(leitor);
                        listaPedidos.Add(pedidoBuscado);
                    }
                }
            }
            return listaPedidos;
        }

        public void AtualizarStatus(Pedido pedidoStatusAtualizado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"UPDATE PEDIDOS SET STATUSPEDIDO = @STATUSPEDIDO
                                WHERE IDPEDIDO = @IDPEDIDO";

                    ConverterObjetoParaSQL(pedidoStatusAtualizado, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public Pedido BuscarPedidoPorId(int idPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM PEDIDOS WHERE IDPEDIDO = @IDPEDIDO;";
                    comando.Parameters.AddWithValue("@IDPEDIDO", idPedido);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var pedidoBuscado = ConverterSqlParaObjeto(leitor);
                        return pedidoBuscado;
                    }
                }
            }
            return null;
        }
        public void ExcluirPedido(Pedido pedidoExcluido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"DELETE FROM PEDIDOS WHERE IDPEDIDO = @IDPEDIDO;";
                    comando.Parameters.AddWithValue("@IDPEDIDO", pedidoExcluido.IdPedido);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }      

        private void ConverterObjetoParaSQL(Pedido novoPedido, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@IDPEDIDO", novoPedido.IdPedido);
            comando.Parameters.AddWithValue("@CPFCLIENTE", novoPedido.CpfCliente);
            comando.Parameters.AddWithValue("@PRODUTOID", novoPedido.ProdutoId);
            comando.Parameters.AddWithValue("@QTDPRODUTO", novoPedido.QtdProduto);
            comando.Parameters.AddWithValue("@DATAPEDIDO", novoPedido.DataPedido);
            comando.Parameters.AddWithValue("@VALORTOTAL", novoPedido.ValorTotal);
            comando.Parameters.AddWithValue("@STATUSPEDIDO", novoPedido.Status);
        }

        private Pedido ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            Pedido pedido = new Pedido();
            pedido.IdPedido = Convert.ToInt32(leitor["IDPEDIDO"].ToString());
            pedido.CpfCliente = leitor["CPFCLIENTE"].ToString();
            pedido.ProdutoId = Convert.ToInt32(leitor["PRODUTOID"].ToString());
            pedido.QtdProduto = Convert.ToInt32(leitor["QTDPRODUTO"].ToString());
            pedido.DataPedido = Convert.ToDateTime(leitor["DATAPEDIDO"].ToString());
            pedido.ValorTotal = Convert.ToDecimal(leitor["VALORTOTAL"].ToString());
            pedido.Status = (Status)Convert.ToInt32(leitor["STATUSPEDIDO"]);
            return pedido;
        }
    }
}
