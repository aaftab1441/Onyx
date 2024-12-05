using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ComOptionModel : FullAuditedEntity, IMustHaveTenant
    {
        public int ComOptionId { get; set; }
        public int ChargepointModelId { get; set; }
        public int TenantId { get; set; }

        public ComOption ComOption { get; set; }
        public ChargepointModel ChargepointModel { get; set; }
    }
}
