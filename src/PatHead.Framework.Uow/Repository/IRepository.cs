using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatHead.Framework.Uow.Entity;

namespace PatHead.Framework.Uow.Repository
{
    public interface ICommonRepository<TEntity> : IQueryRepository<TEntity> where TEntity : IEntity
    {
        public void Add(TEntity entity);
        public Task AddAsync(TEntity entity);
        public void AddAndCommit(TEntity entity);
        public void Remove(TEntity entity);
        public void RemoveRange(List<TEntity> entities);
        public void Update(TEntity entity);
    }

    public interface IQueryRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public IQueryable<TEntity> GetQueryable();
    }

    public interface IRepository<TEntity> where TEntity : IEntity
    {
    }

    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
    }
}