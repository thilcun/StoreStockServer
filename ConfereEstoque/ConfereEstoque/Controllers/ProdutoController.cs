using ConfereEstoque.Contracts;
using ConfereEstoque.Core;
using ConfereEstoque.Data;
using ConfereEstoque.Entities;
using ConfereEstoque.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConfereEstoque.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProdutoController : ApiControllerBase
    {
        
        public ProdutoController()
        {
            PostgresServices = new PostgresServices("192.168.0.6", 5432);
            //PostgresServices = new DummyPostgresServices();
        }
        public IPostgresServices PostgresServices { get; set; }
        [HttpGet]
        public IHttpActionResult Consulta(string q)
        {
            return GetHttpResponse(() => {
                List<ItemProduto> itens = new List<ItemProduto>();

                List<Produto> produtos = new List<Produto>();
                if (q.Count() == 13)
                    produtos.AddRange(PostgresServices.GetProdutosPorEan(q));
                else
                    produtos.AddRange(PostgresServices.GetProdutosPorCodigo(q));

                foreach (Produto p in produtos)
                {
                    ItemAjuste item = AjusteContext.Ajuste.Itens.SingleOrDefault(i => i.Produto.CodigoProduto == p.CodigoProduto);
                    if (item != null)
                    {
                        itens.Add(new ItemProduto { Produto = p, AjusteId = item.AjusteId, Quantidade = item.Quantidade });
                    }
                    else
                    {
                        itens.Add(new ItemProduto { Produto = p, AjusteId = AjusteContext.Ajuste.Codigo, Quantidade = 0 });
                    }
                }
                return Ok(itens);
            });
            
        }
    }
}
