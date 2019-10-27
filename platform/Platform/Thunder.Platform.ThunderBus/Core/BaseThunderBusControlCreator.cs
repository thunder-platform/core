using System;
using MassTransit;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Platform.ThunderBus.Core
{
    public abstract class BaseThunderBusControlCreator : IThunderBusControlCreator
    {
        protected BaseThunderBusControlCreator(
            IServiceProvider serviceProvider,
            IThunderConsumerTypeCache thunderConsumerTypeCache)
        {
            ServiceProvider = serviceProvider;
            ThunderConsumerTypeCache = thunderConsumerTypeCache;
        }

        public IServiceProvider ServiceProvider { get; }

        protected IThunderConsumerTypeCache ThunderConsumerTypeCache { get; }

        public IBusControl Create()
        {
            return InternalCreate();
        }

        protected abstract IBusControl InternalCreate();
    }
}
