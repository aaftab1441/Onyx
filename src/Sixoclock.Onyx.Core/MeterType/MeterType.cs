using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class MeterType : FullAuditedEntity, IMustHaveTenant
    {
        public string Type { get; set; }
        public string Comment { get; set; }

        public int TenantId { get; set; }
        
        public virtual ICollection<ModelEVSE> ModelEVSEs { get; set; }
        public virtual ICollection<EVSE> EVSEs { get; set; }
    }
}
