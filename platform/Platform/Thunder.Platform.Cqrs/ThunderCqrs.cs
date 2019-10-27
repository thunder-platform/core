using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Thunder.Platform.Cqrs
{
    public class ThunderCqrs : IThunderCqrs
    {
        private readonly IMediator _mediator;

        public ThunderCqrs(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <inheritdoc/>
        public async Task SendCommand(BaseThunderCommand command, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(command, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TResult> SendQuery<TResult>(BaseThunderQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SendEvent(BaseThunderEvent @event, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(@event, cancellationToken);
        }
    }
}
