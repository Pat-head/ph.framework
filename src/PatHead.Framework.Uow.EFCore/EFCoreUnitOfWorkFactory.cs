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
        private readonly UnitOfWorkOptions _unitOfWorkOptions;
        private readonly ILogger<EFCoreUnitOfWorkFactory> _logger;

        public EFCoreUnitOfWorkFactory(
            IServiceProvider serviceProvider,
            IOptions<UnitOfWorkOptions> options,
            ILogger<EFCoreUnitOfWorkFactory> logger)
        {
            _logger = logger;
            _unitOfWorkOptions = options.Value;
            _serviceScope = serviceProvider.CreateScope();
        }

        /// <summary>
        /// 获取工作单元
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork GetUnitOfWork()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var registerManagementDbContext = _unitOfWorkOptions.RegisterManagementContext
                .Select(type => (DbContext)_serviceScope.ServiceProvider.GetService(type)).ToList();
            _logger.LogInformation($"current init UnitOfWork DbContexts cost:{stopwatch.ElapsedMilliseconds}ms");
            return new EFCoreUnitOfWork(_serviceScope.ServiceProvider,registerManagementDbContext);
        }
    }
}