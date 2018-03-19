using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Contracts
{
    public interface IPostgresServices
    {
        List<Produto> GetProdutosPorEan(string query);
        List<Produto> GetProdutosPorCodigo(string query);
    }
}
