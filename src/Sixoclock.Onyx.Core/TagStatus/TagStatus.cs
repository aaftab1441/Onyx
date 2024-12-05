using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class TagStatus : FullAuditedEntity, IMustHaveTenant
    {
        public string Status { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
