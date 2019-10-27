using Thunder.Platform.ThunderBus.Core.Abstractions;

namespace Thunder.Service.Timesheet.Contracts
{
    public interface IReportBioDeviceStatusMessage : IThunderMessage
    {
        string DeviceIp { get; }

        bool Available { get; }
    }
}
