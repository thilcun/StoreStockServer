using ConfereEstoque.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, ConfereEstoqueContext>
        where T : class, new()
    {
    }
}
