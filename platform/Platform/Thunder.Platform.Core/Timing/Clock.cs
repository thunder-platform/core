using System;

namespace Thunder.Platform.Core.Timing
{
    public static class Clock
    {
        static Clock()
        {
            Provider = new DotNetClockProvider();
        }

        public static IClockProvider Provider { get; private set; }

        public static DateTime Now => Provider.Now;

        public static DateTimeKind Kind => Provider.Kind;

        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }

        public static void SetProvider(IClockProvider clockProvider)
        {
            Provider = clockProvider ?? throw new ArgumentNullException(nameof(clockProvider));
        }
    }
}
