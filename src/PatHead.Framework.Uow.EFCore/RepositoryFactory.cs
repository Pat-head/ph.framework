using Microsoft.EntityFrameworkCore;
using PatHead.Framework.Uow.Entity;
using PatHead.Framework.Uow.Repository;

namespace PatHead.Framework.Uow.EFCore
{
    public static class RepositoryFactory
    {
        public static ICommonRepository<TEntity> GenerateRepository<TEntity>(DbContext dbContext)
            where TEntity : class, IEntity
        {
            return new BaseCommonRepository<TEntity>(dbContext);
        }
    }
}