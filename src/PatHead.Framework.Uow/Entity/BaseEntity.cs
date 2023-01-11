namespace PatHead.Framework.Uow.Entity
{
    public interface IEntity
    {
    }

    public class BaseGenericKeyEntity<T> : IEntity
    {
        public T Id { get; set; }
    }

    public class BaseIntegerKeyEntity : BaseGenericKeyEntity<int>
    {
    }
}