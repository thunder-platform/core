using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Dependency;
using Thunder.Platform.Core.Exceptions;

namespace Thunder.Platform.Core.Modules
{
    public abstract class ThunderModule : IThunderModule
    {
        /// <summary>
        /// The platform will call this method to bootstrap module.
        /// Basically, this is an entry point of the module registration process.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public virtual void RegisterTo(IServiceCollection services)
        {
            ApplyConventionalRegistrarFor(services);
            InternalRegisterTo(services);
        }

        internal static bool IsThunderModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IThunderModule).IsAssignableFrom(type);
        }

        internal static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsThunderModule(moduleType))
            {
                throw new GeneralException("This type is not an Thunder module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    list.AddRange(dependsOnAttribute.DependedModuleTypes);
                }
            }

            return list;
        }

        protected virtual void InternalRegisterTo(IServiceCollection services)
        {
        }

        protected virtual List<IConventionalDependencyRegistrar> DeclareConventionalRegistrars()
        {
            return new List<IConventionalDependencyRegistrar>
            {
                new BasicConventionalRegistrar()
            };
        }

        private void ApplyConventionalRegistrarFor(IServiceCollection services)
        {
            var conventionalRegistrars = DeclareConventionalRegistrars();
            foreach (IConventionalDependencyRegistrar conventionalRegistrar in conventionalRegistrars)
            {
                var registrationContext = new ConventionalRegistrationContext(GetType().Assembly, services);
                conventionalRegistrar.RegisterAssembly(registrationContext);
            }
        }
    }
}
