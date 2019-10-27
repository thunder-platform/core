using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Dependency;
using Thunder.Platform.Core.Modules;
using Thunder.Platform.ThunderBus.Dependency;

[assembly: ThunderModuleAssembly]

namespace Thunder.Hosting.TestMq
{
    [StartupModule]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
            services.AddThunderRabbitMqService();
        }

        protected override List<IConventionalDependencyRegistrar> DeclareConventionalRegistrars()
        {
            var registrars = base.DeclareConventionalRegistrars();
            registrars.Add(new ThunderBusConventionalRegistrar());

            return registrars;
        }
    }
}
