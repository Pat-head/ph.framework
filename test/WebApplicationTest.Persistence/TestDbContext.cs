using Microsoft.EntityFrameworkCore;
using WebApplicationTest.Domain.Entities;

namespace WebApplicationTest.Persistence
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
        public virtual DbSet<DemoEntity> DemoEntities { get; set; }
    }
    
    
    public class Test2DbContext : DbContext
    {
        public Test2DbContext(DbContextOptions<Test2DbContext> options) : base(options)
        {
        }
        public virtual DbSet<DemoEntity> DemoEntities { get; set; }
    }
}