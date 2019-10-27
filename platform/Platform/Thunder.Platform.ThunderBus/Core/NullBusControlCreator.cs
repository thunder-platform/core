using MassTransit;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Platform.ThunderBus.Core
{
    internal class NullBusControlCreator : IThunderBusControlCreator
    {
        public IBusControl Create()
        {
            return new NullBusControl();
        }
    }
}
