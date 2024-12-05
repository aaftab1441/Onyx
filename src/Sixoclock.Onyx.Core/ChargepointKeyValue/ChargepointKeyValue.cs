using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ChargepointKeyValue : FullAuditedEntity, IMustHaveTenant
    {
        public string ChargepointValue { get; set; }
        public string WildValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }

        public int ChargepointId { get; set; }
        public int KeyValueId { get; set; }
        public int TenantId { get; set; }

        public virtual Chargepoint Chargepoint { get; set; }
        public string FeatureName { get; set; }
        public string Key { get; set; }
    }
}
