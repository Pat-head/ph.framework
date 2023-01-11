using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PatHead.Framework.Uow.Attributes;
using PatHead.Framework.Uow.Repository;

namespace PatHead.Framework.Uow
{
    public static class UnitOfWorkExtensions
    {
        public static IServiceCollection AddUnitOfWork<TUnitOfWorkFactory>(this IServiceCollection services,
            Action<UnitOfWorkOptions> options) where TUnitOfWorkFactory : class, IUnitOfWorkFactory
        {
            UnitOfWorkManager.AddAction(options);
            services.AddScoped<IUnitOfWorkFactory, TUnitOfWorkFactory>();
            ConfigRepository(services, options);
            return services;
        }

        private static void ConfigRepository(IServiceCollection services, Action<UnitOfWorkOptions> optionsAction)
        {
            UnitOfWorkOptions unitOfWorkOptions = new UnitOfWorkOptions();
            optionsAction(unitOfWorkOptions);

            #region ScanRepositoryAssembly

            if (unitOfWorkOptions.ScanRepositoryAssembly.Any())
            {
                var assemblyDic = AppDomain.CurrentDomain.GetAssemblies()
                    .ToDictionary(x => x.GetName().Name, x => x);

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
                                x.GetCustomAttributes(typeof(RepositoryAttribute), false).Any()
                            )
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
        /// <summary>
        /// UnitOfWork Name
        /// </summary>
        public string Name { get; set; } = "default";

        public List<Type> RegisterManagementContext { get; set; }
        public List<string> ScanRepositoryAssembly { get; set; }
    }
}