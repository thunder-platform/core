using System;
using MediatR;
using Thunder.Platform.Core.Timing;

namespace Thunder.Platform.Cqrs
{
    public abstract class BaseThunderEvent : INotification
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime CreatedDate { get; } = Clock.Now;
    }
}
