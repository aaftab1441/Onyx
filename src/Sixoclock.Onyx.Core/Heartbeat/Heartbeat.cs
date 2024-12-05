using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class Heartbeat : FullAuditedEntity, IMustHaveTenant
    {
        public DateTime Time { get; set; }

        public int ChargepointId { get; set; }
        public int TenantId { get; set; }

        public virtual Chargepoint Connector { get; set; }
    }
}
