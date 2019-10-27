using Microsoft.Extensions.DependencyInjection.Extensions;
using Thunder.Platform.ThunderBus.Core.Abstractions;
using Thunder.Platform.ThunderBus.RabbitMq;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ThunderRabbitMqServiceCollectionExtensions
    {
        public static IServiceCollection AddThunderRabbitMqService(this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton<IThunderBusControlCreator, RabbitMqBusControlCreator>());

            services.Replace(ServiceDescriptor.Singleton(provider =>
            {
                var creator = provider.GetRequiredService<IThunderBusControlCreator>();

                return creator.Create();
            }));

            return services;
        }
    }
}
