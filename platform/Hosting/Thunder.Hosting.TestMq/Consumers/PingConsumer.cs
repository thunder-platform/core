using System;
using System.Threading.Tasks;
using Serilog;
using Thunder.Platform.ThunderBus.Core;

namespace Thunder.Hosting.TestMq.Consumers
{
    [Consumer("thunder_ping")]
    public class PingConsumer : BaseThunderConsumer<IPingMessage>
    {
        public PingConsumer(ILogger logger) : base(logger)
        {
        }

        protected override async Task InternalHandleAsync(IPingMessage message)
        {
            await Console.Out.WriteLineAsync($"Received message with content: {message.Ping}");
        }
    }
}
