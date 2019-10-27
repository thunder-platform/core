using System;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using GreenPipes.Util;
using MassTransit;

namespace Thunder.Platform.ThunderBus.Core
{
    internal class NullSendEndpoint : ISendEndpoint
    {
        public ConnectHandle ConnectSendObserver(ISendObserver observer)
        {
            return new EmptyConnectHandle();
        }

        public Task Send<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            return Task.CompletedTask;
        }

        public Task Send<T>(T message, IPipe<SendContext<T>> pipe, CancellationToken cancellationToken = default) where T : class
        {
             return Task.CompletedTask;
        }

        public Task Send<T>(T message, IPipe<SendContext> pipe, CancellationToken cancellationToken = default) where T : class
        {
             return Task.CompletedTask;
        }

        public Task Send(object message, CancellationToken cancellationToken = default)
        {
             return Task.CompletedTask;
        }

        public Task Send(object message, Type messageType, CancellationToken cancellationToken = default)
        {
             return Task.CompletedTask;
        }

        public Task Send(object message, IPipe<SendContext> pipe, CancellationToken cancellationToken = default)
        {
             return Task.CompletedTask;
        }

        public Task Send(object message, Type messageType, IPipe<SendContext> pipe, CancellationToken cancellationToken = default)
        {
             return Task.CompletedTask;
        }

        public Task Send<T>(object values, CancellationToken cancellationToken = default) where T : class
        {
             return Task.CompletedTask;
        }

        public Task Send<T>(object values, IPipe<SendContext<T>> pipe, CancellationToken cancellationToken = default) where T : class
        {
             return Task.CompletedTask;
        }

        public Task Send<T>(object values, IPipe<SendContext> pipe, CancellationToken cancellationToken = default) where T : class
        {
             return Task.CompletedTask;
        }
    }
}
