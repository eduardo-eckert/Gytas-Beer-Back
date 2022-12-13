using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cervejaria.Domain;

namespace Cervejaria.Infra.Data.DAO
{
    public class ProdutoDAO
    {
        private readonly string _connectionString =
            @"server=.\SQLexpress;initial catalog=CERVEJARIA;integrated security=true;";

        public void CadastrarProduto(Produto novoProduto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"INSERT PRODUTOS(DESCRICAO, PRECO, QTDESTOQUE, DATAVALIDADE) 
                        VALUES (@DESCRICAO, @PRECO, @QTDESTOQUE, @DATAVALIDADE)";

                    ConverterObjetoParaSQL(novoProduto, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EditarProduto(Produto produtoEditado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"UPDATE PRODUTOS SET DESCRICAO = @DESCRICAO, PRECO = @PRECO, DATAVALIDADE = @DATAVALIDADE
                                WHERE IDPRODUTO = @IDPRODUTO";

                    ConverterObjetoParaSQL(produtoEditado, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Produto> BuscarTodosProdutos()
        {
            var listaProdutos = new List<Produto>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM PRODUTOS;";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var produtoBuscado = ConverterSqlParaObjeto(leitor);
                        listaProdutos.Add(produtoBuscado);
                    }
                }
            }
            return listaProdutos;
        }

        public List<Produto> BuscarTodosProdutosAtivos()
        {
            var listaProdutos = new List<Produto>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM PRODUTOS WHERE ATIVO = 1;";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var produtoBuscado = ConverterSqlParaObjeto(leitor);
                        listaProdutos.Add(produtoBuscado);
                    }
                }
            }
            return listaProdutos;
        }

        public void GerenciarEstoque(Produto produtoGerenciado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"UPDATE PRODUTOS SET QTDESTOQUE = @QTDESTOQUE
                                WHERE IDPRODUTO = @IDPRODUTO";

                    ConverterObjetoParaSQL(produtoGerenciado, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtivarEDesativarProduto(Produto produtoAtivadoDesativado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"UPDATE PRODUTOS SET ATIVO = @ATIVO
                                WHERE IDPRODUTO = @IDPRODUTO";

                    ConverterObjetoParaSQL(produtoAtivadoDesativado, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public Produto BuscarProdutoPorId(int idProduto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM PRODUTOS WHERE IDPRODUTO = @IDPRODUTO;";
                    comando.Parameters.AddWithValue("@IDPRODUTO", idProduto);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var produtoBuscado = ConverterSqlParaObjeto(leitor);
                        return produtoBuscado;
                    }
                }
            }
            return null;
        }

        public bool VerificarProdutoPorDescricao(string descricaoProduto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM PRODUTOS WHERE DESCRICAO = @DESCRICAO;";
                    comando.Parameters.AddWithValue("@DESCRICAO", descricaoProduto);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var produtoBuscado = ConverterSqlParaObjeto(leitor);
                        return true;
                    }
                }
            }
            return false;
        }

        private void ConverterObjetoParaSQL(Produto novoProduto, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@IDPRODUTO", novoProduto.IdProduto);
            comando.Parameters.AddWithValue("@DESCRICAO", novoProduto.Descricao);
            comando.Parameters.AddWithValue("@PRECO", novoProduto.Preco);
            comando.Parameters.AddWithValue("@QTDESTOQUE", novoProduto.QtdEstoque);
            comando.Parameters.AddWithValue("@DATAVALIDADE", novoProduto.DataValidade);
            comando.Parameters.AddWithValue("@ATIVO", novoProduto.Ativo);
        }

        private Produto ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            Produto produto = new Produto();
            produto.IdProduto = Convert.ToInt32(leitor["IDPRODUTO"].ToString());
            produto.Descricao = leitor["DESCRICAO"].ToString();
            produto.Preco = Convert.ToDecimal(leitor["PRECO"].ToString());
            produto.QtdEstoque = Convert.ToInt32(leitor["QTDESTOQUE"].ToString());
            produto.DataValidade = Convert.ToDateTime(leitor["DATAVALIDADE"].ToString());
            produto.Ativo = Convert.ToBoolean(leitor["ATIVO"].ToString());
            return produto;
        }
    }
}
