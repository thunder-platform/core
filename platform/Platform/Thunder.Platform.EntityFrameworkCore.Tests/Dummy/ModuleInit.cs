using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Thunder.Platform.Core.Domain.Repositories;
using Thunder.Platform.Core.Modules;

[assembly: ThunderModuleAssembly]

namespace Thunder.Platform.EntityFrameworkCore.Tests.Dummy
{
    [StartupModule]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
            services.AddTransient<IDbContextSeeder, SampleSeeder>();
            services.AddTransient<IEntityTypeMapper, SampleMapper>();

            services.Replace(ServiceDescriptor.Singleton<IDbContextResolver, TestDbContextResolver>());

            services.AddEntityFrameworkSqlite()
                .AddDbContext<TestSqliteDbContext>(ServiceLifetime.Transient);

            services.AddTransient(typeof(IRepository<>), typeof(TestBaseRepository<>));
        }
    }
}
