using System.Linq;
using Microsoft.EntityFrameworkCore;
using PatHead.Framework.Uow.EFCore;
using WebApplicationTest.Domain;
using WebApplicationTest.Domain.Entities;

namespace WebApplicationTest.Persistence
{
    public class DemoRepository : BaseRepository<DemoEntity>, IDemoRepository
    {
        public DemoRepository(TestDbContext testDbContext) : base(testDbContext)
        {
        }
    }
}