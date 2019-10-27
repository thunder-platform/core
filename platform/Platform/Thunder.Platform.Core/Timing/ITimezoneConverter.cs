using System;

namespace Thunder.Platform.Core.Timing
{
    public interface ITimezoneConverter
    {
        DateTime? ConvertFromUtc(DateTime? date, string timezone);
    }
}
