using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Dependency;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Platform.ThunderBus.Dependency
{
    public class ThunderBusConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.Services.Scan(scan => scan
                .FromAssemblies(context.Assembly)

                // Register transient instances.
                .AddClasses(@class => @class.AssignableTo<IThunderMessageHandler>())
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithTransientLifetime());

            context.Services.AddMassTransit(serviceConfig =>
            {
                serviceConfig.AddConsumers(context.Assembly);
            });
        }
    }
}
