using System;

namespace Thunder.Platform.Core.Domain.Auditing
{
    public interface IHasDeletedDate
    {
        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        DateTime? DeletedDate { get; set; }
    }
}
