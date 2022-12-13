using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Cervejaria.Domain;

namespace Cervejaria.Infra.Data.DAO
{
    public class ClienteDAO
    {
        private readonly string _connectionString =
            @"server=.\SQLexpress;initial catalog=CERVEJARIA;integrated security=true;";

        public void CadastrarCliente(Cliente novoCliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"INSERT CLIENTES(NOME, CPF, PTSFIDELIDADE, DATANASCIMENTO) VALUES (@NOME, @CPF, @PTSFIDELIDADE, @DATANASCIMENTO)";

                    ConverterObjetoParaSQL(novoCliente, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EditarCliente(Cliente clienteEditado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"UPDATE CLIENTES SET NOME = @NOME, CPF = @CPF, DATANASCIMENTO = @DATANASCIMENTO
                                WHERE IDCLIENTE = @IDCLIENTE";

                    ConverterObjetoParaSQL(clienteEditado, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarPontos(Cliente clientePontosAtualizado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql =
                        @"UPDATE CLIENTES SET PTSFIDELIDADE = @PTSFIDELIDADE WHERE IDCLIENTE = @IDCLIENTE";

                    ConverterObjetoParaSQL(clientePontosAtualizado, comando);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> BuscarTodosClientes()
        {
            var listaClientes = new List<Cliente>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM CLIENTES;";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var clienteBuscado = ConverterSqlParaObjeto(leitor);
                        listaClientes.Add(clienteBuscado);
                    }
                }
            }
            return listaClientes;
        }

        public Cliente BuscarClientePorCpf(string cpfBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM CLIENTES WHERE CPF = @CPF;";
                    comando.Parameters.AddWithValue("@CPF", cpfBuscado);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var clienteBuscado = ConverterSqlParaObjeto(leitor);
                        return clienteBuscado;
                    }
                }
            }
            return null;
        }
        public Cliente BuscarClientePorId(int idBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM CLIENTES WHERE IDCLIENTE = @IDCLIENTE;";
                    comando.Parameters.AddWithValue("@IDCLIENTE", idBuscado);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var clienteBuscado = ConverterSqlParaObjeto(leitor);
                        return clienteBuscado;
                    }
                }
            }
            return null;
        }
        public bool VerificarClientePorCpf(string cpfBuscado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM CLIENTES WHERE CPF = @CPF;";
                    comando.Parameters.AddWithValue("@CPF", cpfBuscado);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var clienteBuscado = ConverterSqlParaObjeto(leitor);
                        return true;
                    }
                }
            }
            return false;
        }

        public void ExcluirCliente(Cliente clienteExcluido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"DELETE FROM CLIENTES WHERE IDCLIENTE = @IDCLIENTE;";
                    comando.Parameters.AddWithValue("@IDCLIENTE", clienteExcluido.IdCliente);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        private void ConverterObjetoParaSQL(Cliente novoCliente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@IDCLIENTE", novoCliente.IdCliente);
            comando.Parameters.AddWithValue("@NOME", novoCliente.Nome);
            comando.Parameters.AddWithValue("@CPF", novoCliente.CpfCliente);
            comando.Parameters.AddWithValue("@PTSFIDELIDADE", novoCliente.PtsFidelidade);
            comando.Parameters.AddWithValue("@DATANASCIMENTO", novoCliente.DataNascimento);
        }

        private Cliente ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            Cliente cliente = new Cliente();
            cliente.IdCliente = Convert.ToInt32(leitor["IDCLIENTE"].ToString());
            cliente.Nome = leitor["NOME"].ToString();
            cliente.CpfCliente = leitor["CPF"].ToString();
            cliente.PtsFidelidade = Convert.ToDecimal(leitor["PTSFIDELIDADE"].ToString());
            cliente.DataNascimento = Convert.ToDateTime(leitor["DATANASCIMENTO"].ToString());
            return cliente;
        }
    }
}
