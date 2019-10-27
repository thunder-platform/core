using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core;
using Thunder.Platform.Core.Modules;
using Thunder.Platform.ThunderBus.Core;
using Thunder.Platform.ThunderBus.Core.Abstractions;

[assembly: ThunderModuleAssembly]

namespace Thunder.Platform.ThunderBus
{
    [DependsOn(typeof(ThunderKernelModule))]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
            services.AddSingleton<IThunderBusControlCreator, NullBusControlCreator>();

            services.AddSingleton(provider =>
            {
                var creator = provider.GetRequiredService<IThunderBusControlCreator>();

                return creator.Create();
            });

            services.AddSingleton<IThunderProducer, ThunderProducer>();
        }
    }
}
