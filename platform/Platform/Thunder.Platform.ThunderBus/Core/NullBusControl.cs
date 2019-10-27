using System;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using GreenPipes.Util;
using MassTransit;
using MassTransit.Topology;

namespace Thunder.Platform.ThunderBus.Core
{
    internal class NullBusControl : IBusControl
    {
        public Uri Address => null;

        public IBusTopology Topology => null;

        public ConnectHandle ConnectConsumeMessageObserver<T>(IConsumeMessageObserver<T> observer) where T : class
        {
            return new EmptyConnectHandle();
        }

        public ConnectHandle ConnectConsumeObserver(IConsumeObserver observer)
        {
            return new EmptyConnectHandle();
        }

        public ConnectHandle ConnectConsumePipe<T>(IPipe<ConsumeContext<T>> pipe) where T : class
        {
            return new EmptyConnectHandle();
        }

        public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
        {
            return new EmptyConnectHandle();
        }

        public ConnectHandle ConnectReceiveEndpointObserver(IReceiveEndpointObserver observer)
        {
            return new EmptyConnectHandle();
        }

        public ConnectHandle ConnectReceiveObserver(IReceiveObserver observer)
        {
            return new EmptyConnectHandle();
        }

        public ConnectHandle ConnectRequestPipe<T>(Guid requestId, IPipe<ConsumeContext<T>> pipe) where T : class
        {
            return new EmptyConnectHandle();
        }

        public ConnectHandle ConnectSendObserver(ISendObserver observer)
        {
            return new EmptyConnectHandle();
        }

        public Task<ISendEndpoint> GetSendEndpoint(Uri address)
        {
            return Task.FromResult(new NullSendEndpoint() as ISendEndpoint);
        }

        public void Probe(ProbeContext context)
        {
        }

        public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            return Task.CompletedTask;
        }

        public Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class
        {
            return Task.CompletedTask;
        }

        public Task Publish<T>(T message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class
        {
            return Task.CompletedTask;
        }

        public Task Publish(object message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish(object message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish(object message, Type messageType, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish<T>(object values, CancellationToken cancellationToken = default) where T : class
        {
            return Task.CompletedTask;
        }

        public Task Publish<T>(object values, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class
        {
            return Task.CompletedTask;
        }

        public Task Publish<T>(object values, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class
        {
            return Task.CompletedTask;
        }

        public Task<BusHandle> StartAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new NullBusHandle() as BusHandle);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
