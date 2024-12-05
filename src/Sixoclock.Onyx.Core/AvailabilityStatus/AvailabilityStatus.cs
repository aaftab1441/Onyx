using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class AvailabilityStatus: FullAuditedEntity,IMustHaveTenant
    {
        public string Value { get; set; }
        public int TenantId { get; set; }
        public virtual ICollection<AvailabilityEvent> AvailabilityEvents { get; set; }
    }
}
