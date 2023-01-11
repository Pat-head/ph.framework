using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationTest.Domain.Entities;

namespace WebApplicationTest.Persistence
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public virtual DbSet<DemoEntity> DemoEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DemoEntityConfiguration());
        }
    }


    public class Test2DbContext : DbContext
    {
        public Test2DbContext(DbContextOptions<Test2DbContext> options) : base(options)
        {
        }

        public virtual DbSet<DemoEntity> DemoEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DemoEntityConfiguration());
        }
    }


    public class DemoEntityConfiguration : IEntityTypeConfiguration<DemoEntity>
    {
        public void Configure(EntityTypeBuilder<DemoEntity> builder)
        {
            builder.ToTable("demo", schema: "public");

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                .HasColumnName("id");
            
            builder.Property(x => x.Name)
                .HasColumnName("name");
        }
    }
}