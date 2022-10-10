using PatHead.Framework.Repository;
using WebApplicationTest.Domain.Entities;

namespace WebApplicationTest.Domain
{
    public interface IDemoRepository : IRepository<DemoEntity>
    {
    }
}