using ConfereEstoque.Common;
using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Data
{
    public abstract class ItemAjusteRepositoryBase<T, U> : DataRepositoryBase<T>
        where T : ItemAjuste, new()
        where U : DbContext, new()
    {
        protected abstract IEnumerable<T> GetEntities(U entityContext, int ajusteId, int produtoId);

        public IEnumerable<T> Get(int ajusteId, int produtoId)
        {
            using (U entityContext = new U())
                return (GetEntities(entityContext, ajusteId, produtoId)).ToArray().ToList();
        }
    }
}
