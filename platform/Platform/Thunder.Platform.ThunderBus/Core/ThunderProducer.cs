using System;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Serilog;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Platform.ThunderBus.Core
{
    public class ThunderProducer : IThunderProducer, IDisposable
    {
        private readonly IBusControl _busControl;
        private readonly ILogger _logger;
        private readonly string _hostUri;

        private bool _disposed;

        public ThunderProducer(IBusControl busControl, ILogger logger)
        {
            _busControl = busControl;
            _logger = logger;
            _hostUri = ConstructHostUri(busControl.Address);
        }

        public Task PublishAsync<TMessage>(object message) where TMessage : class, IThunderMessage
        {
            return _busControl.Publish<TMessage>(message);
        }

        public async Task SendAsync<TMessage>(object message, string routeKey) where TMessage : class, IThunderMessage
        {
            var sendEndpoint = await _busControl.GetSendEndpoint(new Uri($"{_hostUri}{routeKey}"));
            await sendEndpoint.Send<TMessage>(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _busControl?.Stop();
                }

                _disposed = true;
            }
        }

        private string ConstructHostUri(Uri busAddress)
        {
            var hostUrIStrBuilder = new StringBuilder($"{busAddress.Scheme}://{busAddress.Host}");

            // Skip the last segment, the receiving endpoint
            for (int i = 0; i < busAddress.Segments.Length - 1; i++)
            {
                hostUrIStrBuilder.Append(busAddress.Segments[i]);
            }

            var hostUri = hostUrIStrBuilder.ToString();

            _logger.Information($"Thunder bus's constructed with host uri: {hostUri}");

            return hostUri;
        }
    }
}
