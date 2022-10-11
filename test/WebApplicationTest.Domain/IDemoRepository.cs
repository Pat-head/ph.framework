using PatHead.Framework.Uow.Repository;
using WebApplicationTest.Domain.Entities;

namespace WebApplicationTest.Domain
{
    public interface IDemoRepository : IRepository<DemoEntity>
    {
    }
}