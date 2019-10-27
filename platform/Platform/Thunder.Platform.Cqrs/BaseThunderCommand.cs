using System;
using MediatR;
using Thunder.Platform.Core.Timing;

namespace Thunder.Platform.Cqrs
{
    public abstract class BaseThunderCommand : IRequest
    {
        public Guid CommandId { get; } = Guid.NewGuid();

        public DateTime CreatedDate { get; } = Clock.Now;
    }
}
