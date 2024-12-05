using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ClearCacheStatus:FullAuditedEntity,IMustHaveTenant
    {
        public string Value { get; set; }
        public int TenantId { get; set; }
        public virtual ICollection<ClearCacheEvent> ClearCacheEvents { get; set; }
    }
}
