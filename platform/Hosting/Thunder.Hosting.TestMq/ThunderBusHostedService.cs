using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Thunder.Hosting.TestMq.Consumers;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Hosting.TestMq
{
    public class ThunderBusHostedService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly IThunderProducer _thunderProducer;

        public ThunderBusHostedService(IBusControl busControl, IThunderProducer thunderProducer)
        {
            _busControl = busControl;
            _thunderProducer = thunderProducer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);

            await _thunderProducer.SendAsync<IPingMessage>(
                new
                {
                    MessageId = Guid.NewGuid(),
                    Ping = "this is from routing key"
                },
                "thunder_ping");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_busControl != null)
            {
                await _busControl.StopAsync(cancellationToken);
            }
        }
    }
}
