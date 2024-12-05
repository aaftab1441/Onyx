using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class ResetStatus : FullAuditedEntity, IMustHaveTenant
    {
        public string ResetStatusValue { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<ResetEvent> ResetEvents { get; set; }
    }
}
