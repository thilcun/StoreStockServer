using ConfereEstoque.Contracts;
using ConfereEstoque.Data;
using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Data_Repositories
{
    [Export(typeof(IItemAjusteRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ItemAjusteRepository : ItemAjusteRepositoryBase<ItemAjuste, ConfereEstoqueContext>, IItemAjusteRepository
    {
        protected override ItemAjuste AddEntity(ConfereEstoqueContext entityContext, ItemAjuste entity)
        {
            return entityContext.ItemAjusteSet.Add(entity);
        }

        protected override ItemAjuste UpdateEntity(ConfereEstoqueContext entityContext, ItemAjuste entity)
        {
            return (from e in entityContext.ItemAjusteSet
                    where e.Codigo == entity.Codigo
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ItemAjuste> GetEntities(ConfereEstoqueContext entityContext)
        {
            return from e in entityContext.ItemAjusteSet
                   select e;
        }

        protected override ItemAjuste GetEntity(ConfereEstoqueContext entityContext, int id)
        {
            var query = (from e in entityContext.ItemAjusteSet
                         where e.Codigo == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override IEnumerable<ItemAjuste> GetEntities(ConfereEstoqueContext entityContext, int ajusteId, int produtoId)
        {
            var query = (from e in entityContext.ItemAjusteSet
                         where e.AjusteId == ajusteId
                         && e.Produto.CodigoProduto == produtoId
                         select e);
            return query;
        }
    }
}
