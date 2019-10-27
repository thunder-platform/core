using Serilog.Events;

namespace Thunder.Platform.Core.Logging
{
    public class ThunderLoggingOptions
    {
        public LogEventLevel LogRestrictedToMinimumLevel { get; set; }

        public int LogFileSizeLimitBytes { get; set; }

        public string LogOutputTemplate { get; set; }

        public string LogPathFormat { get; set; }

        public int? RetainedFileCountLimit { get; set; }
    }
}
