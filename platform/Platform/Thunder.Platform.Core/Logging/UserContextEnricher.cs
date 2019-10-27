using System;
using Serilog.Core;
using Serilog.Events;
using Thunder.Platform.Core.Constants;
using Thunder.Platform.Core.Context;

namespace Thunder.Platform.Core.Logging
{
    internal class UserContextEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null)
            {
                throw new ArgumentNullException(nameof(logEvent));
            }

            T ContextValueOf<T>(string contextKey)
            {
                return UserContext.Current.GetValue<T>(contextKey);
            }

            logEvent.AddProperty(CommonConstants.TenantId, ContextValueOf<string>(CommonUserContextKeys.TenantId));
            logEvent.AddProperty(CommonConstants.RequestId, ContextValueOf<string>(CommonUserContextKeys.RequestId));
            logEvent.AddProperty(CommonConstants.OriginIp, ContextValueOf<string>(CommonUserContextKeys.OriginIp));
            logEvent.AddProperty(CommonConstants.Username, ContextValueOf<string>(CommonUserContextKeys.Username));
            logEvent.AddProperty(CommonConstants.Environment, ContextValueOf<string>(CommonUserContextKeys.Environment));
            logEvent.AddProperty(CommonConstants.ProcessId, ContextValueOf<string>(CommonUserContextKeys.ProcessId));
        }
    }
}
