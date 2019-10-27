using System;
using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Service.Timesheet.Contracts
{
    public interface ITimesheetRecordMessage : IThunderMessage
    {
        /// <summary>
        /// idEnrollNumberText.
        /// </summary>
        string TimesheetId { get; }

        /// <summary>
        /// The time when the employee check in the biometric system.
        /// </summary>
        DateTime CheckedTime { get; }

        string DeviceIp { get; }
    }
}
