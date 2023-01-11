using System;
using System.Threading;
using System.Threading.Tasks;
using PatHead.Framework.Uow.Entity;
using PatHead.Framework.Uow.Repository;

namespace PatHead.Framework.Uow
{
    public interface IUnitOfWork
    {
        public ICommonRepository<TEntity> GetSimpleRepository<TEntity>() where TEntity : class, IEntity;

        public T GetRepository<T>() where T : class;

        ITransactionWrapper BeginTransaction();

        void Commit();

        Task CommitAsync(CancellationToken cancellationToken = default);
    }

    public interface ITransactionWrapper
    {
        void Commit();
        void Rollback();
    }
}