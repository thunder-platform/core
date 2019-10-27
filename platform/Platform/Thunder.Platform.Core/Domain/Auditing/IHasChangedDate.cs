using System;

namespace Thunder.Platform.Core.Domain.Auditing
{
    public interface IHasChangedDate
    {
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        DateTime? ChangedDate { get; set; }
    }
}
