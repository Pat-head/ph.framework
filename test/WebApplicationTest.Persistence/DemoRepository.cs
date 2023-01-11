using System.Linq;
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
        public DemoRepository(TestDbContext testDbContext) : base(testDbContext)
        {
        }
    }
}