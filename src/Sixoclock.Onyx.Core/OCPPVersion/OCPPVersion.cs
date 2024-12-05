using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class OCPPVersion : FullAuditedEntity, IMustHaveTenant
    {
        public string VersionName { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<ChargepointModel> ChargepointModels { get; set; }
        public virtual ICollection<OCPPMessage> OCPPMessages { get; set; }
        public virtual ICollection<OCPPFeature> OCPPFeatures { get; set; }
        public virtual ICollection<Chargepoint> Chargepoints { get; set; }
    }
}
