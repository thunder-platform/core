using System;
using MediatR;
using Thunder.Platform.Core.Timing;

namespace Thunder.Platform.Cqrs
{
#pragma warning disable SA1649 // File name should match first type name
    public abstract class BaseThunderQuery<TResult> : IRequest<TResult>
#pragma warning restore SA1649 // File name should match first type name
    {
        public Guid QueryId { get; } = Guid.NewGuid();

        public DateTime CreatedDate { get; } = Clock.Now;
    }
}
