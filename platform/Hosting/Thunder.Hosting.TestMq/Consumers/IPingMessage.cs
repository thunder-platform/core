using System;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Hosting.TestMq.Consumers
{
    public interface IPingMessage : IThunderMessage
    {
        string Ping { get; }
    }
}
