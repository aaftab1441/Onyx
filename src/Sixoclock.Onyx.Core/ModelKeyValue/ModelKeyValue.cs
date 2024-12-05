using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class ModelKeyValue : FullAuditedEntity, IMustHaveTenant
    {
        public string ModelValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }
        public int ChargepointModelId { get; set; }
        public int TenantId { get; set; }

        public string Key { get; set; }
        public string FeatureName { get; set; }
        public int KeyValueId { get; set; }
        
        public virtual ChargepointModel ChargepointModel { get; set; }
    }
}
