using ConfereEstoque.Common;
using ConfereEstoque.Contracts;
using ConfereEstoque.Data;
using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Data_Repositories
{
    [Export(typeof(IAjusteRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AjusteRepository : DataRepositoryBase<Ajuste>, IAjusteRepository
    {
        protected override Ajuste AddEntity(ConfereEstoqueContext entityContext, Ajuste entity)
        {
            return entityContext.AjusteSet.Add(entity);
        }

        protected override Ajuste UpdateEntity(ConfereEstoqueContext entityContext, Ajuste entity)
        {
            return (from e in entityContext.AjusteSet
                    where e.Codigo == entity.Codigo
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Ajuste> GetEntities(ConfereEstoqueContext entityContext)
        {
            
            var result = (from e in entityContext.AjusteSet
                          select e).Include(r => r.Itens);

            return result;
        }

        protected override Ajuste GetEntity(ConfereEstoqueContext entityContext, int id)
        {
            var query = (from e in entityContext.AjusteSet
                         where e.Codigo == id
                         select e).Include(r => r.Itens);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
