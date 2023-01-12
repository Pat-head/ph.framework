using System.Threading.Tasks;
using PatHead.Framework.Uow.Repository;
using WebApplicationTest.Domain.Entities;

namespace WebApplicationTest.Domain
{
    public interface IDemoRepository : ICommonRepository<DemoEntity>
    {
        Task Run();
    }
}