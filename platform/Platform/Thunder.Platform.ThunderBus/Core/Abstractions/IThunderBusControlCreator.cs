using MassTransit;

namespace Thunder.Platform.ThunderBus.Core.Abstractions
{
    public interface IThunderBusControlCreator
    {
        IBusControl Create();
    }
}
