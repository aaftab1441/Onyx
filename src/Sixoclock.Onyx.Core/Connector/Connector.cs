using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Connector))]
    public class Connector : FullAuditedEntity, IMustHaveTenant
    {
        public int EVSEId { get; set; }
        public int ConnectorTypeId { get; set; }
        public int CapacityId { get; set; }
        [Ruleable(typeof(string))]
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual EVSE EVSE { get; set; }
        public virtual ConnectorType ConnectorType { get; set; }
        public virtual Capacity Capacity { get; set; }
    }
}
