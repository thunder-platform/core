using System;

namespace Thunder.Platform.Core.Timing
{
    public interface IClockProvider
    {
        DateTime Now { get; }

        DateTimeKind Kind { get; }

        DateTime Normalize(DateTime dateTime);
    }
}
