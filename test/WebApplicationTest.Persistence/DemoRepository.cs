using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatHead.Framework.Uow.Attributes;
using PatHead.Framework.Uow.EFCore;
using PatHead.Framework.Uow.Repository;
using WebApplicationTest.Domain;
using WebApplicationTest.Domain.Entities;

namespace WebApplicationTest.Persistence
{
    [Repository]
    public class DemoRepository : BaseCommonRepository<DemoEntity>, IDemoRepository
    {
        private readonly TestDbContext _testDbContext;

        public DemoRepository(TestDbContext testDbContext) : base(testDbContext)
        {
            _testDbContext = testDbContext;
        }

        public async Task Run()
        {
            _testDbContext.Database.SqlQuery<string>("SELECT name FROM public.demo", null);
            await _testDbContext.DemoEntities.ToListAsync();
        }
    }
}