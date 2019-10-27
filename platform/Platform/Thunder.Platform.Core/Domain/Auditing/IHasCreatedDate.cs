using System;

namespace Thunder.Platform.Core.Domain.Auditing
{
    public interface IHasCreatedDate
    {
        DateTime CreatedDate { get; set; }
    }
}
