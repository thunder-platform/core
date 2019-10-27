using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Platform.ThunderBus.Core
{
    public class ThunderConsumerTypeCache : IThunderConsumerTypeCache
    {
        private readonly ConcurrentDictionary<string, Type> _consumers;

        public ThunderConsumerTypeCache()
        {
            _consumers = new ConcurrentDictionary<string, Type>();
        }

        public void Register(string name, Type type)
        {
            GetOrAdd(name, type);
        }

        public ImmutableDictionary<string, Type> GetConsumers()
        {
            return _consumers.ToImmutableDictionary();
        }

        private Type GetOrAdd(string consumerRoutingKey, Type type)
        {
            return _consumers.GetOrAdd(consumerRoutingKey, type);
        }
    }
}
