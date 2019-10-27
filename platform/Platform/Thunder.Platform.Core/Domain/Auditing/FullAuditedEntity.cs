using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thunder.Platform.Core.Domain.Auditing
{
    public abstract class FullAuditedEntity : AuditedEntity, IFullAudited
    {
        [Column(Order = 1002)]
        public DateTime? DeletedDate { get; set; }
    }
}
