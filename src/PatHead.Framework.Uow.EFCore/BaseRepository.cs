using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatHead.Framework.Uow.Entity;
using PatHead.Framework.Uow.Repository;

namespace PatHead.Framework.Uow.EFCore
{
    public class BaseCommonRepository<TEntity> : BaseQueryRepository<TEntity>, ICommonRepository<TEntity>
        where TEntity : class, IEntity
    {
        public BaseCommonRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(List<TEntity> entities)
        {
           DbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }
    }

    public class BaseQueryRepository<TEntity> : BaseRepository<TEntity>, IQueryRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly DbSet<TEntity> DbSet;

        public BaseQueryRepository(DbContext dbContext)
        {
            DbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable();
        }
    }

    public static class QueryableEfCoreExtensions
    {
        public static Task<List<TEntity>> ToListAsync<TEntity>(this IQueryable<TEntity> queryable)
        {
            return EntityFrameworkQueryableExtensions.ToListAsync(queryable);
        }
    }
}