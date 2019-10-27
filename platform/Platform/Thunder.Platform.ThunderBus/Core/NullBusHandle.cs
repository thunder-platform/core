using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace Thunder.Platform.ThunderBus.Core
{
    internal class NullBusHandle : BusHandle
    {
        public Task<BusReady> Ready { get; }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
