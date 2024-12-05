using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class OCPPFeature : FullAuditedEntity, IMustHaveTenant
    {
        public string FeatureName { get; set; }
        public string Comment { get; set; }
        public int OCPPVersionId { get; set; }
        public int TenantId { get; set; }
        
        public virtual ICollection<KeyValue> KeyValues { get; set; }
        public virtual OCPPVersion OCPPVersion { get; set; }
    }
}
