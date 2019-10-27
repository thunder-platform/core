using System;
using Serilog;
using Serilog.Configuration;

namespace Thunder.Platform.Core.Logging
{
    internal static class UserContextLoggerConfigurationExtensions
    {
        public static LoggerConfiguration FromUserContext(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null)
            {
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            }

            return enrichmentConfiguration.With<UserContextEnricher>();
        }
    }
}
