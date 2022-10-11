using Microsoft.EntityFrameworkCore;
using PatHead.Framework.Uow.Repository;

namespace PatHead.Framework.Uow.EFCore
{
    public static class RepositoryFactory
    {
        public static IRepository<TEntity> GenerateRepository<TEntity>(DbContext dbContext) where TEntity : class
        {
            return new BaseRepository<TEntity>(dbContext);
        }
    }
}