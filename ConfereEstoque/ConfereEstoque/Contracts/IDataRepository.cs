using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Contracts
{
    public interface IDataRepository
    {
    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        T Add(T entity);

        void Remove(T entity);

        void Remove(int id);

        T Update(T entity);

        IEnumerable<T> Get();

        Task<IEnumerable<T>> GetAsync();

        T Get(int id);
 
    }

}
