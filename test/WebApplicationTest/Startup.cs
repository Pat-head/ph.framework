using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PatHead.Framework.Uow;
using PatHead.Framework.Uow.EFCore;
using WebApplicationTest.Domain;
using WebApplicationTest.Persistence;

namespace WebApplicationTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<DomainService>();

            services.AddUnitOfWork<EFCoreUnitOfWorkFactory>(options =>
            {
                options.RegisterManagementContext = new List<Type>()
                {
                    typeof(TestDbContext), typeof(Test2DbContext)
                };
                
                options.RepositoryGenericType = typeof(BaseRepository<>);

                options.ScanRepositoryAssembly = new List<string>()
                {
                    "WebApplicationTest.Persistence"
                };
            });

            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseNpgsql("?")
                    .UseSnakeCaseNamingConvention();
            });

            services.AddDbContext<Test2DbContext>(options =>
            {
                options.UseNpgsql("?")
                    .UseSnakeCaseNamingConvention();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}