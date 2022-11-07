namespace PatHead.Framework.Uow
{
    public interface IUnitOfWorkFactory
    {
        public IUnitOfWork GetUnitOfWork(string name = "default");
    }
}