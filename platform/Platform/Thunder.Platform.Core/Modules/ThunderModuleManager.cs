using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Exceptions;

namespace Thunder.Platform.Core.Modules
{
    public class ThunderModuleManager : IThunderModuleManager
    {
        private readonly IEnumerable<IThunderModule> _moduleInstances;
        private readonly Type _startupModuleType;

        private ThunderModuleCollection _modules;

        public ThunderModuleManager(IEnumerable<IThunderModule> moduleInstances, Type startupModuleType)
        {
            _moduleInstances = moduleInstances;
            _startupModuleType = startupModuleType;
        }

        public void Initialize(IServiceCollection services)
        {
            _modules = new ThunderModuleCollection(_startupModuleType);

            LoadAllModules(_moduleInstances);

            var sortedModuleList = _modules.GetSortedModuleListByDependency();
            foreach (var module in sortedModuleList)
            {
                module.Instance.RegisterTo(services);
            }
        }

        private void LoadAllModules(IEnumerable<IThunderModule> modules)
        {
            _modules.AddRange(modules.Select(m => new ThunderModuleInfo(m.GetType(), m)));
            _modules.EnsureKernelModuleToBeFirst();
            _modules.EnsureStartupModuleToBeLast();

            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                // Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in ThunderModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new GeneralException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if (moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null)
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
    }
}
