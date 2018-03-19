using ConfereEstoque.Contracts;
using ConfereEstoque.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Data
{
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        
        T IDataRepositoryFactory.GetDataRepository<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
