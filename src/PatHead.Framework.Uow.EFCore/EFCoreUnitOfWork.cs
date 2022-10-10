using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatHead.Framework.Repository;

namespace PatHead.Framework.Uow.EFCore
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private readonly List<DbContext> _dbContexts;
        private readonly IServiceProvider _serviceProvider;

        public EFCoreUnitOfWork(IServiceProvider serviceProvider, List<DbContext> dbContexts)
        {
            _serviceProvider = serviceProvider;
            _dbContexts = dbContexts;
        }

        public IRepository<TEntity> GetSimpleRepository<TEntity>() where TEntity : class
        {
            foreach (var dbContext in _dbContexts)
            {
                foreach (var propertyInfo in dbContext.GetType().GetProperties())
                {
                    if (propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                    {
                        if (propertyInfo.PropertyType.GenericTypeArguments[0].FullName == typeof(TEntity).FullName)
                        {
                            return RepositoryFactory.GenerateRepository<TEntity>(dbContext);
                        }
                    }
                }
            }

            return null;
        }

        public T GetRepository<T>() where T : class
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        public TransactionWrapper<object> BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _dbContexts.ForEach(x => x.SaveChanges());
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            foreach (var dbContext in _dbContexts)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}