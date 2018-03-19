using ConfereEstoque.Contracts;
using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Data
{
    [Export(typeof(IPostgresServices))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DummyPostgresServices : IPostgresServices
    {
        public List<Produto> GetProdutosPorEan(string query)
        {
            return new List<Produto>
            {
                new Produto{
                    CodigoProduto = Convert.ToInt32(query),
                    CodigoBarra = "1234567890987",
                    Descricao = "Produto Test",
                    Marca = new Marca{ Codigo = 1234, Descricao = "Marca Test"},
                    Ncm = "21341234",
                    ValorUnitario = 12.23m
                }
            };
        }

        public List<Produto> GetProdutosPorCodigo(string query)
        {
            return GetProdutosPorEan(query);
        }
    }
}
