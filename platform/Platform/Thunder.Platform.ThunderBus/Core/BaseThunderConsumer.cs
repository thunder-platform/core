using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MassTransit;
using Polly;
using Polly.CircuitBreaker;
using Serilog;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Platform.ThunderBus.Core
{
    public abstract class BaseThunderConsumer<TMessage> : IThunderConsumer<TMessage>
        where TMessage : class, IThunderMessage
    {
        private readonly ILogger _logger;
        private readonly CircuitBreakerPolicy _circuitBreakerPolicy;
        private bool _disposed;

        protected BaseThunderConsumer(ILogger logger)
        {
            _logger = logger;

            _circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromMinutes(2));
        }

        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await HandleAsync(context.Message);
        }

        public async Task HandleAsync(TMessage message)
        {
            var stopwatch = new Stopwatch();

            try
            {
                _logger.Information($"Begin to handle message...");
                await _circuitBreakerPolicy.Execute(() => InternalHandleAsync(message));
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"There is an exception when trying to send the message. Details: {exception}");
            }
            finally
            {
                stopwatch.Stop();
                _logger.Information($"End handling message. Total time: {stopwatch.ElapsedMilliseconds} milliseconds.");
            }
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
                _disposed = true;
            }
        }

        protected abstract Task InternalHandleAsync(TMessage message);
    }
}
