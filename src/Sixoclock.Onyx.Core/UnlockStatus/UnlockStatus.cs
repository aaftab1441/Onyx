using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class UnlockStatus : FullAuditedEntity, IMustHaveTenant
    {
        public string Value { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<UnlockEvent> UnlockEvents { get; set; }
    }
}
