using System.Diagnostics.CodeAnalysis;
using Serilog.Events;

namespace Thunder.Platform.Core.Logging
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Only framework team can decide to use property injection.")]
    [SuppressMessage("StyleCop", "SA1402", Justification = "Only framework team can decide to use property injection.")]
    internal static class LogEventForUserContextExtensions
    {
        public static void AddProperty(this LogEvent logEvent, string property, object value)
        {
            logEvent.AddOrUpdateProperty(new LogEventProperty(property, new ScalarValue(value)));
        }
    }
}
