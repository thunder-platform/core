using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Thunder.Service.HostedService
{
    public class AzureServiceBusConsumerService : IHostedService, IDisposable
    {
        private readonly IBusControl _busControl;

        public AzureServiceBusConsumerService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync(cancellationToken);
        }

        public void Dispose()
        {
        }
    }
}
