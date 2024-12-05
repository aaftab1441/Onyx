using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ModelFeature : FullAuditedEntity, IMustHaveTenant
    {
        public int ChargepointModelId { get; set; }
        public int OCPPFeatureId { get; set; }
        public int TenantId { get; set; }
        
        public virtual ChargepointModel ChargepointModel { get; set; }
    }
}
