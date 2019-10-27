using System;
using MassTransit;
using Microsoft.Extensions.Options;
using Thunder.Platform.Core.Exceptions;
using Thunder.Platform.ThunderBus.Core;
using Thunder.Platform.ThunderBus.Core.Abstractions;
using Thunder.Platform.ThunderBus.Options;

namespace Thunder.Platform.ThunderBus.RabbitMq
{
    public class RabbitMqBusControlCreator : BaseThunderBusControlCreator
    {
        private readonly RabbitMqOptions _options;

        public RabbitMqBusControlCreator(
            IOptions<RabbitMqOptions> options,
            IServiceProvider serviceProvider,
            IThunderConsumerTypeCache thunderConsumerTypeCache)
            : base(serviceProvider, thunderConsumerTypeCache)
        {
            _options = options.Value;
        }

        protected override IBusControl InternalCreate()
        {
            // To understand how MassTransit create/setup RabbitMQ,
            // please refer to: https://stackoverflow.com/questions/56074676/how-to-override-masstransit-default-exchange-and-queue-topology-convention.
            return Bus.Factory.CreateUsingRabbitMq(busFactoryConfigurator =>
            {
                if (string.IsNullOrEmpty(_options.ConnectionString))
                {
                    throw new GeneralException("RabbitMQ connection string is missing.");
                }

                var builder = new RabbitMqConnectionStringBuilder(_options.ConnectionString);
                var host = busFactoryConfigurator.Host(new Uri(builder.Host), hostConfigurator =>
                {
                    hostConfigurator.Username(builder.Username);
                    hostConfigurator.Password(builder.Password);
                    hostConfigurator.Heartbeat(10);
                });

                busFactoryConfigurator.AutoDelete = true;

                // You may see the following logic is duplicated code with AzureServiceBusControlCreator.cs
                // But trust me, the type of parameters in ReceiveEndpoint method are different between 2 type of queues.
                var consumers = ThunderConsumerTypeCache.GetConsumers();
                foreach (var consumersKey in consumers.Keys)
                {
                    var consumerType = consumers[consumersKey];
                    busFactoryConfigurator.ReceiveEndpoint(
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
