using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Thunder.Platform.Cqrs
{
#pragma warning disable SA1649 // File name should match first type name
    public abstract class BaseThunderQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
#pragma warning restore SA1649 // File name should match first type name
        where TQuery : BaseThunderQuery<TResult>
    {
        public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await HandleAsync(request, cancellationToken);
        }

        protected abstract Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
