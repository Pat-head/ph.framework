using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace PatHead.Framework.Uow
{
    public static class UnitOfWorkExtensions
    {
        public static IServiceCollection AddUnitOfWork<TUnitOfWorkFactory>(this IServiceCollection services,
            Action<UnitOfWorkOptions> options) where TUnitOfWorkFactory : class, IUnitOfWorkFactory
        {
            services.Configure(options);
            services.AddScoped<IUnitOfWorkFactory, TUnitOfWorkFactory>();
            ConfigRepository(services);
            return services;
        }

        private static void ConfigRepository(IServiceCollection services)
        {
            var buildServiceProvider = services.BuildServiceProvider();
            var service = buildServiceProvider.GetService<IOptions<UnitOfWorkOptions>>();
            var unitOfWorkOptions = service.Value;

            #region ScanRepositoryAssembly

            if (unitOfWorkOptions.ScanRepositoryAssembly.Any())
            {
                var assemblyDic = AppDomain.CurrentDomain.GetAssemblies()
                    .ToDictionary(x => x.GetName().Name, x => x);
                var repositoryGenericType = unitOfWorkOptions.RepositoryGenericType;
                foreach (var repositoryAssemblyName in unitOfWorkOptions.ScanRepositoryAssembly)
                {
                    if (assemblyDic.ContainsKey(repositoryAssemblyName))
                    {
                        var assembly = assemblyDic[repositoryAssemblyName];

                        var types = assembly
                            .GetTypes()
                            .Where(x =>
                                !x.IsGenericType &&
                                !x.IsAbstract &&
                                x.IsClass &&
                                x.BaseType != null &&
                                x.BaseType.IsGenericType &&
                                x.BaseType.GetGenericTypeDefinition() == repositoryGenericType)
                            .ToList();

                        foreach (var type in types)
                        {
                            var baseType = type.GetInterfaces().FirstOrDefault(t => t.Name == $"I{type.Name}");
                            services.TryAddScoped(baseType, type);
                        }
                    }
                }
            }

            #endregion
        }
    }

    public class UnitOfWorkOptions
    {
        public List<Type> RegisterManagementContext { get; set; }
        public Type RepositoryGenericType { get; set; }
        public List<string> ScanRepositoryAssembly { get; set; }
    }
}