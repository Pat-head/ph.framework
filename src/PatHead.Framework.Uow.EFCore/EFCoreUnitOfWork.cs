using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PatHead.Framework.Uow.Entity;
using PatHead.Framework.Uow.Repository;

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

        public ICommonRepository<TEntity> GetSimpleRepository<TEntity>() where TEntity : class, IEntity
        {
            foreach (var dbContext in _dbContexts)
            {
                foreach (var propertyInfo in dbContext.GetType().GetProperties())
                {
                    if (propertyInfo.PropertyType.IsGenericType)
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
            }
            return null;
        }

        public T GetRepository<T>() where T : class
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        private List<IDbContextTransaction> InSideBeginTransaction()
        {
            return _dbContexts.Select(dbContext => dbContext.Database.BeginTransaction()).ToList();
        }

        public ITransactionWrapper BeginTransaction()
        {
            var inSideBeginTransaction = InSideBeginTransaction();
            return new TransactionWrapper(inSideBeginTransaction);
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

    public class TransactionWrapper : ITransactionWrapper
    {
        public TransactionWrapper(List<IDbContextTransaction> inSideBeginTransaction)
        {
            Delegate = inSideBeginTransaction;
        }

        private List<IDbContextTransaction> Delegate { get; set; }

        public void Commit()
        {
            Delegate.ForEach(x => x.Commit());
        }

        public void Rollback()
        {
            Delegate.ForEach(x => x.Rollback());
        }
    }
}