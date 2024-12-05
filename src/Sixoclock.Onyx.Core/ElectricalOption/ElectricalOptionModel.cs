using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ElectricalOptionModel : FullAuditedEntity, IMustHaveTenant
    {
        public int ElectricalOptionId { get; set; }
        public int ChargepointModelId { get; set; }
        public int TenantId { get; set; }

        public ElectricalOption ElectricalOption { get; set; }
        public ChargepointModel ChargepointModel { get; set; }
    }
}