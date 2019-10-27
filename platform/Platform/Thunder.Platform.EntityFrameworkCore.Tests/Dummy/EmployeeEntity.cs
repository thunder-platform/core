using Thunder.Platform.Core.Domain.Auditing;

namespace Thunder.Platform.EntityFrameworkCore.Tests.Dummy
{
    public class EmployeeEntity : FullAuditedEntity
    {
        public string Name { get; set; }
    }
}
