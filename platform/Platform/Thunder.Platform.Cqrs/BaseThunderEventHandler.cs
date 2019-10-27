using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Thunder.Platform.Cqrs
{
#pragma warning disable SA1649 // File name should match first type name
    public abstract class BaseThunderEventHandler<TEvent> : INotificationHandler<TEvent>
#pragma warning restore SA1649 // File name should match first type name
        where TEvent : BaseThunderEvent
    {
        public async Task Handle(TEvent request, CancellationToken cancellationToken)
        {
            await HandleAsync(request, cancellationToken);
        }

        protected abstract Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
    }
}
