using System;
using Thunder.Platform.Core.Dependency;

namespace Thunder.Platform.Core.Timing
{
    public class TimezoneConverter : ITimezoneConverter, ITransientInstance
    {
        public DateTime? ConvertFromUtc(DateTime? date, string timezone)
        {
            if (!date.HasValue)
            {
                return null;
            }

            if (string.IsNullOrEmpty(timezone))
            {
                return date;
            }

            TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById("UTC");
            TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return TimeZoneInfo.ConvertTime(date.Value, sourceTimeZone, destinationTimeZone);
        }
    }
}
