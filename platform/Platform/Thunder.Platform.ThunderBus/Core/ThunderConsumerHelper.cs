using System;
using System.Reflection;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Platform.ThunderBus.Core
{
    public static class ThunderConsumerHelper
    {
        public static bool IsThunderConsumer(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IThunderMessageHandler).IsAssignableFrom(type);
        }
    }
}
