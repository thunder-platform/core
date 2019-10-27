using System;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Extensions.Options;
using Thunder.Platform.ThunderBus.Core;
using Thunder.Platform.ThunderBus.Core.Abstractions;
using Thunder.Platform.ThunderBus.Options;

namespace Thunder.Platform.ThunderBus.AzureServiceBus
{
    public class AzureServiceBusControlCreator : BaseThunderBusControlCreator
    {
        private readonly AzureServiceBusOptions _options;

        public AzureServiceBusControlCreator(
            IOptions<AzureServiceBusOptions> options,
            IServiceProvider serviceProvider,
            IThunderConsumerTypeCache thunderConsumerTypeCache)
            : base(serviceProvider, thunderConsumerTypeCache)
        {
            _options = options.Value;
        }

        protected override IBusControl InternalCreate()
        {
            return Bus.Factory.CreateUsingAzureServiceBus(cfg =>
            {
                cfg.PrefetchCount = _options.PrefetchCount ?? 16;
                cfg.DefaultMessageTimeToLive = TimeSpan.FromDays(_options.TokenTimeToLive ?? 1);

                var host = cfg.Host(_options.BusUri, h =>
                {
                    h.SharedAccessSignature(s =>
                    {
                        s.KeyName = _options.BusKeyName;
                        s.SharedAccessKey = _options.BusSharedAccessKey;
                        s.TokenTimeToLive = TimeSpan.FromDays(_options.TokenTimeToLive ?? 1);
                        s.TokenScope = _options.TokenScope;
                    });
                });

                var consumers = ThunderConsumerTypeCache.GetConsumers();
                foreach (var consumersKey in consumers.Keys)
                {
                    var consumerType = consumers[consumersKey];
                    cfg.ReceiveEndpoint(
                        host,
                        consumersKey,
                        ec =>
                        {
                            ec.ConfigureConsumer(ServiceProvider, consumerType);
                        });
                }
            });
        }
    }
}
