using System;
using System.Linq;
using System.Reflection;
using Thunder.Platform.Core;
using Thunder.Platform.Core.Exceptions;
using Thunder.Platform.Core.Modules;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ThunderModuleServiceCollectionExtensions
    {
        public static IServiceCollection AddThunderModuleSystem(this IServiceCollection services)
        {
            var assemblies = ThunderAssemblyFinder.FromAppContext();
            var thunderModuleTypes = assemblies.SelectMany(assembly => assembly.GetTypes().Where(ThunderModule.IsThunderModule)).ToList();

            var startupModuleType = thunderModuleTypes.SingleOrDefault(t => t.GetCustomAttribute(typeof(StartupModuleAttribute)) != null);
            if (startupModuleType == null)
            {
                throw new GeneralException("Please specify the startup module by marking [StartupModule] in StartupModule.cs or ModuleInit.cs");
            }

            var thunderModuleInstances = thunderModuleTypes.Select(type => (IThunderModule)Activator.CreateInstance(type));
            var thunderModuleManager = new ThunderModuleManager(thunderModuleInstances, startupModuleType);

            thunderModuleManager.Initialize(services);

            services.AddSingleton<IThunderModuleManager>(provider => thunderModuleManager);

            return services;
        }
    }
}
