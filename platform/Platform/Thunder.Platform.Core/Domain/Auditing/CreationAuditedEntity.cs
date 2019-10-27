using System;
using System.ComponentModel.DataAnnotations.Schema;
using Thunder.Platform.Core.Domain.Entities;

namespace Thunder.Platform.Core.Domain.Auditing
{
    public abstract class CreationAuditedEntity : Entity, ICreationAudited
    {
        [Column(Order = 1000)]
        public DateTime CreatedDate { get; set; }
    }
}
