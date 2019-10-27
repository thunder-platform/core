using System;
using System.Threading.Tasks;
using MassTransit;

namespace Thunder.Platform.ThunderBus.Core.Abstractions
{
    public interface IThunderConsumer<in TMessage> :
        IConsumer<TMessage>,
        IDisposable,
        IThunderMessageHandler where TMessage : class, IThunderMessage
    {
        Task HandleAsync(TMessage command);
    }
}
