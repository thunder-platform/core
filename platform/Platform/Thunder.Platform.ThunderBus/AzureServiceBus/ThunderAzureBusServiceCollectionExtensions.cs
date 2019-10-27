using Microsoft.Extensions.DependencyInjection.Extensions;
using Thunder.Platform.ThunderBus.AzureServiceBus;
using Thunder.Platform.ThunderBus.Core.Abstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ThunderAzureBusServiceCollectionExtensions
    {
        public static IServiceCollection AddThunderAzureBusService(this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton<IThunderBusControlCreator, AzureServiceBusControlCreator>());

            services.Replace(ServiceDescriptor.Singleton(provider =>
            {
                var creator = provider.GetRequiredService<IThunderBusControlCreator>();

                return creator.Create();
            }));

            return services;
        }
    }
}
