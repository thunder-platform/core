using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Thunder.Platform.Core.Modules;

namespace Thunder.Platform.Core
{
    public class ThunderAssemblyFinder
    {
        public static Assembly[] FromAppContext()
        {
            if (DependencyContext.Default == null)
            {
                return FromAppDomain();
            }

            return FromDependencyContext(DependencyContext.Default);
        }

        public static Assembly[] FromDependencyContext(DependencyContext context)
        {
            Guard.NotNull(context, nameof(context));

            var assemblies = context.RuntimeLibraries
                .SelectMany(library => library.GetDefaultAssemblyNames(context))
                .Select(Assembly.Load)
                .Where(IsThunderModule())
                .ToArray();

            return assemblies;
        }

        public static Assembly[] FromAppDomain()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(IsThunderModule()).ToArray();
        }

        private static Func<Assembly, bool> IsThunderModule()
        {
            return assembly => assembly.GetCustomAttribute(typeof(ThunderModuleAssemblyAttribute)) != null;
        }
    }
}
