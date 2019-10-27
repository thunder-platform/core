using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thunder.Platform.EntityFrameworkCore.Tests.Dummy
{
    public class SampleMapper : BaseEntityTypeMapper<EmployeeEntity>
    {
        protected override void InternalMap(EntityTypeBuilder<EmployeeEntity> builder)
        {
        }
    }
}
