using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class OtherOptionModel : FullAuditedEntity, IMustHaveTenant
    {
        public int OtherOptionId { get; set; }
        public int ChargepointModelId { get; set; }
        public int TenantId { get; set; }

        public OtherOption OtherOption { get; set; }
        public ChargepointModel ChargepointModel { get; set; }
    }
}