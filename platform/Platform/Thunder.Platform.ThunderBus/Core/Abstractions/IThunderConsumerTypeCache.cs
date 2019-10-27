using System;
using System.Collections.Immutable;

namespace Thunder.Platform.ThunderBus.Core.Abstractions
{
    public interface IThunderConsumerTypeCache
    {
        void Register(string name, Type type);

        ImmutableDictionary<string, Type> GetConsumers();
    }
}
