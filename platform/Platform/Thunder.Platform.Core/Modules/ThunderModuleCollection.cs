using System;
using System.Collections.Generic;
using System.Linq;
using Thunder.Platform.Core.Exceptions;
using Thunder.Platform.Core.Extensions;

namespace Thunder.Platform.Core.Modules
{
    public class ThunderModuleCollection : List<ThunderModuleInfo>
    {
        public ThunderModuleCollection(Type startupModuleType)
        {
            StartupModuleType = startupModuleType;
        }

        public Type StartupModuleType { get; }

        public TModule GetModule<TModule>() where TModule : ThunderModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module == null)
            {
                throw new GeneralException("Can not find module for " + typeof(TModule).FullName);
            }

            return (TModule)module.Instance;
        }

        /// <summary>
        /// Sorts modules according to dependencies.
        /// If module A depends on module B, A comes after B in the returned List.
        /// </summary>
        /// <returns>Sorted list.</returns>
        public List<ThunderModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            EnsureStartupModuleToBeLast(sortedModules, StartupModuleType);
            return sortedModules;
        }

        public void EnsureKernelModuleToBeFirst(List<ThunderModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof(ThunderKernelModule));
            if (kernelModuleIndex <= 0)
            {
                // It's already the first!
                return;
            }

            var kernelModule = modules[kernelModuleIndex];
            modules.RemoveAt(kernelModuleIndex);
            modules.Insert(0, kernelModule);
        }

        public void EnsureStartupModuleToBeLast(List<ThunderModuleInfo> modules, Type startupModuleType)
        {
            var startupModuleIndex = modules.FindIndex(m => m.Type == startupModuleType);
            if (startupModuleIndex >= modules.Count - 1)
            {
                // It's already the last!
                return;
            }

            var startupModule = modules[startupModuleIndex];
            modules.RemoveAt(startupModuleIndex);
            modules.Add(startupModule);
        }

        public void EnsureKernelModuleToBeFirst()
        {
            EnsureKernelModuleToBeFirst(this);
        }

        public void EnsureStartupModuleToBeLast()
        {
            EnsureStartupModuleToBeLast(this, StartupModuleType);
        }
    }
}
