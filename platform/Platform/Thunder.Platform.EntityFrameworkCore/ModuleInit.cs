using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Thunder.Platform.Core.Modules;
using Thunder.Platform.EntityFrameworkCore.UnitOfWork;

[assembly: ThunderModuleAssembly]

namespace Thunder.Platform.EntityFrameworkCore
{
    [DependsOn(typeof(ThunderKernelModule))]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
            services.AddTransient<IEfCoreTransactionStrategy, EfCoreTransactionStrategy>();
            services.AddTransient<IUnitOfWork, EfCoreUnitOfWork>();

            // To avoid DI check when migrating to AspNetCore 3.0
            services.AddSingleton<IDbContextResolver, NullDbContextResolver>();

            services.AddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));
        }
    }
}
