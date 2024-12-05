using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx
{
    public class AdminStatusEvent : FullAuditedEntity, IMustHaveTenant
    {
        public string AdminStatus { get; set; }
        public DateTime EventDate { get; set; }
        public int ChargepointId { get; set; }
        public int TenantId { get; set; }

        public virtual Chargepoint Chargepoint { get; set; }
    }
}
