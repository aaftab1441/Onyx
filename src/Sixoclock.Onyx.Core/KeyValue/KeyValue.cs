using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class KeyValue : FullAuditedEntity, IMustHaveTenant
    {
        public string Key { get; set; }
        public string DefaultValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }
        public int OCPPFeatureId { get; set; }
        public int TenantId { get; set; }
        
        public virtual OCPPFeature OCPPFeature { get; set; }
    }
}
