using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Thunder.Platform.Core.Dependency
{
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        internal ConventionalRegistrationContext(Assembly assembly, IServiceCollection services)
        {
            Assembly = assembly;
            Services = services;
        }

        /// <summary>
        /// Gets the registering Assembly.
        /// </summary>
        public Assembly Assembly { get; }

        public IServiceCollection Services { get; }
    }
}
