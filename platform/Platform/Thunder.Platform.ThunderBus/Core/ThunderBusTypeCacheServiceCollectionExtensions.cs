using System;
using System.Linq;
using System.Reflection;
using Thunder.Platform.Core;
using Thunder.Platform.ThunderBus.Core;
using Thunder.Platform.ThunderBus.Core.Abstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ThunderBusTypeCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddThunderBusConsumers(this IServiceCollection services)
        {
            var cache = new ThunderConsumerTypeCache();
            var assemblies = ThunderAssemblyFinder.FromAppContext();

            var thunderConsumerTypes = assemblies.SelectMany(assembly => assembly.GetTypes().Where(ThunderConsumerHelper.IsThunderConsumer)).ToList();
            foreach (var thunderConsumerType in thunderConsumerTypes)
            {
                var consumerAttribute = thunderConsumerType.GetCustomAttribute<ConsumerAttribute>();

                if (consumerAttribute == null)
                {
                    throw new ArgumentNullException($"Please declare {nameof(ConsumerAttribute)} for the consumer class.");
                }

                cache.Register(consumerAttribute.Name, thunderConsumerType);
            }

            services.AddSingleton<IThunderConsumerTypeCache>(cache);

            return services;
        }
    }
}
