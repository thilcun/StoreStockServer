using ConfereEstoque.Data;
using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Contracts
{
    public interface IItemAjusteRepository : IDataRepository<ItemAjuste>
    {
        IEnumerable<ItemAjuste> Get(int ajusteId, int produtoId);
    }

}
