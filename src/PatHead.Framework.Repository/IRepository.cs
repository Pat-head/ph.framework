using System;
using System.Linq;

namespace PatHead.Framework.Repository
{
    public interface IRepository<TEntity>
    {
        public void Add(TEntity entity);

        public IQueryable<TEntity> GetQueryable();

        public void Remove(TEntity entity);

        public void Update(TEntity entity);
    }
}