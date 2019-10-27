using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Context;
using Thunder.Platform.Core.Domain.UnitOfWork;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Thunder.Platform.Core.Modules;

[assembly: ThunderModuleAssembly]

namespace Thunder.Platform.Core
{
    public class ThunderKernelModule : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
            services.AddOptions();

            // builder.RegisterInstance(UserContext.Current).As<IUserContext>().AsImplementedInterfaces().ExternallyOwned();
            services.AddSingleton(UserContext.Current);

            services.AddTransient<IConnectionStringResolver, DefaultConnectionStringResolver>();
            services.AddTransient<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>();
            services.AddTransient<ICurrentUnitOfWorkProvider, AsyncLocalCurrentUnitOfWorkProvider>();
            services.AddTransient<IUnitOfWorkManager, UnitOfWorkManager>();
        }
    }
}
