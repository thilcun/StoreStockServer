using ConfereEstoque.Contracts;
using ConfereEstoque.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Data
{
    [Export(typeof(IPostgresServices))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostgresServices : IPostgresServices
    {
        private string _ip;
        private int _port;
        private string _connectionString;

        private NpgsqlConnection _connection;

        public PostgresServices(string ip, int port)
        {
            _ip = ip;
            _port = port;
            _connectionString = "Server=" + _ip + ";Port=" + port + ";User Id=postgres;Password=1;Database=matriz;";
            _connection = new NpgsqlConnection(_connectionString);
        }
        public List<Produto> GetProdutosPorEan(string query)
        {
            return TryCatchHandledOperation(() => {
                List<Produto> produtos = new List<Produto>();

                NpgsqlCommand command = new NpgsqlCommand();

                string cmdText = "SELECT DISTINCT p.codigo, p.descricao, cb.ean, p.reffornecedor, p.preco, p.codncm, p.codfornecedor, t.fantasia " +
                                " FROM produto p LEFT OUTER JOIN codigobarra cb " +
                                " ON p.codigo = cb.codproduto " +
                                " INNER JOIN terceiro t " +
                                " ON p.codfornecedor = t.codigo " +
                                " AND cb.ean = '" + query + "' ";
                command.CommandText = cmdText;
                command.Connection = _connection;

                OpenConn();

                NpgsqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    int codigo = Convert.ToInt32(reader["codigo"].ToString());
                    //byte[] bytes = Encoding.UTF8.GetBytes(reader["descricao"].ToString());

                    //string test = Encoding.UTF32.GetString(, 0, buffer.Length);
                    //string test32 = Encoding.UTF32.GetString(reader["descricao"].ToString());
                    string desc = "";
                    try
                    {
                        TextReader rea = reader.GetTextReader(1);
                        desc = rea.ReadToEnd();
                    }
                    catch
                    {
                        desc = "Contem acento!";
                    }
                    //string codigogtinean = reader["ean"].ToString();
                    string reffornecedor = reader["reffornecedor"].ToString();
                    string ncm = reader["codncm"].ToString();
                    decimal valor;
                    if (!decimal.TryParse(reader["preco"].ToString(), NumberStyles.Any, new CultureInfo("en-US"), out valor))
                        valor = -1m;
                    int marcacodigo = int.Parse(reader["codfornecedor"].ToString());
                    string marcadesc = reader["fantasia"].ToString();
                    string barra = reader["ean"].ToString();

                    Produto produto = new Produto
                    {
                        CodigoProduto = codigo,
                        Descricao = desc,
                        CodigoBarra = barra,
                        Ncm = ncm,
                        ValorUnitario = valor,
                        Marca = new Marca { Codigo = marcacodigo, Descricao = marcadesc }
                    };
                    produtos.Add(produto);

                }
                CloseConn();
                return produtos;
            });
        }
        
        protected T TryCatchHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                return codeToExecute.Invoke();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void OpenConn()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error :S");
                //TODO: log error
                throw ex;
            }
        }
        public void CloseConn()
        {
            try
            {
                if (_connection.State != ConnectionState.Closed)
                    _connection.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error :S");
                //TODO: log error
                throw ex;
            }
        }

        public List<Produto> GetProdutosPorCodigo(string query)
        {
            return TryCatchHandledOperation(() => {
                List<Produto> produtos = new List<Produto>();

                NpgsqlCommand command = new NpgsqlCommand();

                string cmdText = "SELECT DISTINCT p.codigo, p.descricao, cb.ean, p.reffornecedor, p.preco, p.codncm, p.codfornecedor, t.fantasia " +
                                " FROM produto p LEFT OUTER JOIN codigobarra cb " +
                                " ON p.codigo = cb.codproduto " +
                                " INNER JOIN terceiro t " +
                                " ON p.codfornecedor = t.codigo " +
                                " AND p.codigo = " + query + "; ";
                command.CommandText = cmdText;
                command.Connection = _connection;

                OpenConn();

                NpgsqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    int codigo = Convert.ToInt32(reader["codigo"].ToString());
                    //byte[] bytes = Encoding.UTF8.GetBytes(reader["descricao"].ToString());

                    //string test = Encoding.UTF32.GetString(, 0, buffer.Length);
                    //string test32 = Encoding.UTF32.GetString(reader["descricao"].ToString());
                    string desc = "";
                    try
                    {
                        TextReader rea = reader.GetTextReader(1);
                        desc = rea.ReadToEnd();
                    }
                    catch
                    {
                        desc = "Contem acento!";
                    }
                    //string codigogtinean = reader["ean"].ToString();
                    string reffornecedor = reader["reffornecedor"].ToString();
                    string ncm = reader["codncm"].ToString();
                    decimal valor;
                    if (!decimal.TryParse(reader["preco"].ToString(), NumberStyles.Any, new CultureInfo("en-US"), out valor))
                        valor = -1m;
                    int marcacodigo = int.Parse(reader["codfornecedor"].ToString());
                    string marcadesc = reader["fantasia"].ToString();
                    string barra = reader["ean"].ToString();

                    Produto produto = new Produto
                    {
                        CodigoProduto = codigo,
                        Descricao = desc,
                        CodigoBarra = barra,
                        Ncm = ncm,
                        ValorUnitario = valor,
                        Marca = new Marca { Codigo = marcacodigo, Descricao = marcadesc }
                    };
                    produtos.Add(produto);

                }
                CloseConn();
                return produtos;
            });
        }
    }
}
