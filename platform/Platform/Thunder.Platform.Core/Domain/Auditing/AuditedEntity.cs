using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thunder.Platform.Core.Domain.Auditing
{
    public abstract class AuditedEntity : CreationAuditedEntity, IAudited
    {
        [Column(Order = 1001)]
        public DateTime? ChangedDate { get; set; }
    }
}
