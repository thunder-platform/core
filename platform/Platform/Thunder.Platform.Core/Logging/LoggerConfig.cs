using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Display;

namespace Thunder.Platform.Core.Logging
{
    /// <summary>
    /// This class is to register logger.
    /// </summary>
    public static class LoggerConfig
    {
        public static ILogger Config(ThunderLoggingOptions options)
        {
            var formatter = new MessageTemplateTextFormatter(options.LogOutputTemplate, formatProvider: null);

            // https://github.com/serilog/serilog-sinks-file
            // https://github.com/serilog/serilog/wiki/Enrichment
            // https://github.com/RehanSaeed/Serilog.Exceptions
            ILogger logger = new LoggerConfiguration()
                .Enrich.FromUserContext()
                .Enrich.WithExceptionDetails()

                .WriteTo.File(
                    formatter: formatter,
                    path: options.LogPathFormat,
                    restrictedToMinimumLevel: options.LogRestrictedToMinimumLevel,
                    fileSizeLimitBytes: options.LogFileSizeLimitBytes,
                    retainedFileCountLimit: options.RetainedFileCountLimit,
                    levelSwitch: null,
                    buffered: false,
                    shared: false,
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)

                .WriteTo.Console()

                .CreateLogger();

            return logger;
        }
    }
}
