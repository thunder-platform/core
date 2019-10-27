using Thunder.Platform.Core.Domain.Entities;

namespace Thunder.Platform.Core.Domain.Auditing
{
    public interface IFullAudited : IAudited, IHasDeletedDate
    {
    }
}
