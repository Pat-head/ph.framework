using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PatHead.Framework.Uow.EFCore
{
    public class EFCoreUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceScope _serviceScope;
        private readonly ILogger<EFCoreUnitOfWorkFactory> _logger;

        public EFCoreUnitOfWorkFactory(
            IServiceProvider serviceProvider,
            ILogger<EFCoreUnitOfWorkFactory> logger)
        {
            _logger = logger;
            _serviceScope = serviceProvider.CreateScope();
        }

        /// <summary>
        /// 获取工作单元
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork GetUnitOfWork(string name = "default")
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var unitOfWorkOptions = UnitOfWorkManager.GetUnitOfWorkOptions(name);

            var registerManagementDbContext = unitOfWorkOptions.RegisterManagementContext
                .Select(type => (DbContext)_serviceScope.ServiceProvider.GetService(type)).ToList();

            _logger.LogInformation($"Current init UnitOfWork DbContexts cost:{stopwatch.ElapsedMilliseconds}ms");

            return new EFCoreUnitOfWork(_serviceScope.ServiceProvider, registerManagementDbContext);
        }
    }
}