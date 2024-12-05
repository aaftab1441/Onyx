using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ReleaseOptionModel : FullAuditedEntity, IMustHaveTenant
    {
        public int ChargeReleaseOptionId { get; set; }
        public int ChargepointModelId { get; set; }
        public int TenantId { get; set; }

        public ChargeReleaseOption ChargeReleaseOption { get; set; }
        public ChargepointModel ChargepointModel { get; set; }
    }
}
