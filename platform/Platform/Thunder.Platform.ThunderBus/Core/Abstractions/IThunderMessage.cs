using System;

namespace Thunder.Platform.ThunderBus.Core.Abstractions
{
    public interface IThunderMessage
    {
        Guid MessageId { get; }
    }
}
