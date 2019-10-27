using System.Threading.Tasks;

namespace Thunder.Platform.ThunderBus.Core.Abstractions
{
    public interface IThunderProducer
    {
        Task PublishAsync<TMessage>(object message) where TMessage : class, IThunderMessage;

        Task SendAsync<TMessage>(object message, string routeKey) where TMessage : class, IThunderMessage;
    }
}
