using ConfereEstoque.Contracts;
using ConfereEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Common
{
    public abstract class DataRepositoryBase<T, U> : IDataRepository<T>
        where T : class, new()
        where U : DbContext, new()
    {
        protected abstract T AddEntity(U entityContext, T entity);

        protected abstract T UpdateEntity(U entityContext, T entity);

        protected abstract IEnumerable<T> GetEntities(U entityContext);

        protected abstract T GetEntity(U entityContext, int id);

        public T Add(T entity)
        {
            using (U entityContext = new U())
            {
                T addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public void Remove(T entity)
        {
            using (U entityContext = new U())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (U entityContext = new U())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public T Update(T entity)
        {
            using (U entityContext = new U())
            {
                T existingEntity = UpdateEntity(entityContext, entity);

                SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        public IEnumerable<T> Get()
        {
            using (U entityContext = new U())
                return (GetEntities(entityContext)).ToArray().ToList();
        }
        public async Task<IEnumerable<T>> GetAsync()
        {
            using (U entityContext = new U())
                return await Task.Run(() => (GetEntities(entityContext)).ToArray().AsEnumerable());
        }
        public T Get(int id)
        {
            using (U entityContext = new U())
                return GetEntity(entityContext, id);
        }
    }
}
