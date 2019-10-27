using System;
using System.Diagnostics;

namespace Thunder.Platform.Core
{
    internal struct ValueStopwatch
    {
        private static readonly double _timestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;

        private readonly long _startTimestamp;

        private ValueStopwatch(long startTimestamp) => _startTimestamp = startTimestamp;

        public bool IsActive => _startTimestamp != 0;

        public static ValueStopwatch StartNew() => new ValueStopwatch(Stopwatch.GetTimestamp());

        public TimeSpan GetElapsedTime()
        {
            // Start timestamp can't be zero in an initialized ValueStopwatch. It would have to be literally the first thing executed when the machine boots to be 0.
            // So it being 0 is a clear indication of default(ValueStopwatch)
            if (!IsActive)
            {
                throw new InvalidOperationException("An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");
            }

            long end = Stopwatch.GetTimestamp();
            long timestampDelta = end - _startTimestamp;
            long ticks = (long)(_timestampToTicks * timestampDelta);
            return new TimeSpan(ticks);
        }
    }
}
