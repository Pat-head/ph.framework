using System;
using System.Threading;
using System.Threading.Tasks;
using PatHead.Framework.Uow.Repository;

namespace PatHead.Framework.Uow
{
    public interface IUnitOfWork
    {
        public IRepository<TEntity> GetSimpleRepository<TEntity>() where TEntity : class;

        public T GetRepository<T>() where T : class;

        TransactionWrapper<Object> BeginTransaction();

        void Commit();

        Task CommitAsync(CancellationToken cancellationToken = default);
    }

    public class TransactionWrapper<TObject>
    {
        public TransactionWrapper(TObject inSideBeginTransaction)
        {
            Delegate = inSideBeginTransaction;
        }

        public TObject Delegate { get; set; }
    }
}