namespace PatHead.Framework.Uow
{
    public interface IUnitOfWorkFactory
    {
        public IUnitOfWork GetUnitOfWork();
    }
}