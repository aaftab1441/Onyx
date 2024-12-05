using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ChargepointModelImage : FullAuditedEntity, IMustHaveTenant
    {
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public int ChargepointModelId { get; set; }
        public string OriginalFileName { get; set; }
        public string Ext { get; set; }

        public int TenantId { get; set; }

        public virtual ChargepointModel ChargepointModel { get; set; }
    }
}
