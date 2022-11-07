using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PatHead.Framework.Uow
{
    public static class UnitOfWorkManager
    {
        private static readonly List<Action<UnitOfWorkOptions>> UnitOfWorkOptionsActions =
            new List<Action<UnitOfWorkOptions>>();

        private static readonly List<UnitOfWorkOptions> UnitOfWorkOptionsList = new List<UnitOfWorkOptions>();

        public static void AddAction(Action<UnitOfWorkOptions> action)
        {
            UnitOfWorkOptionsActions.Add(action);
            Build();
        }

        public static UnitOfWorkOptions GetUnitOfWorkOptions(string name)
        {
            var unitOfWorkOptions = UnitOfWorkOptionsList.FirstOrDefault(x => x.Name == name);

            if (unitOfWorkOptions == null)
            {
                throw new DirectoryNotFoundException($"名称: {name} UnitOfWorkOptions 缺失");
            }

            return unitOfWorkOptions;
        }

        private static void Build()
        {
            UnitOfWorkOptionsList.Clear();
            foreach (var action in UnitOfWorkOptionsActions)
            {
                var unitOfWorkOptions = new UnitOfWorkOptions();
                action(unitOfWorkOptions);
                UnitOfWorkOptionsList.Add(unitOfWorkOptions);
            }
        }
    }
}