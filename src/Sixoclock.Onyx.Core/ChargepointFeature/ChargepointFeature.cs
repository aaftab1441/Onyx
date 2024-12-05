using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class ChargepointFeature : FullAuditedEntity, IMustHaveTenant
    {
        public int ChargepointId { get; set; }
        public int OCPPFeatureId { get; set; }
        public int TenantId { get; set; }

        public virtual Chargepoint Chargepoint { get; set; }
    }
}
