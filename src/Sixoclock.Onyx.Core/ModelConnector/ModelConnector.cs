using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ModelConnector : FullAuditedEntity, IMustHaveTenant
    {
        public int ModelEVSEId { get; set; }
        public int CapacityId { get; set; }
        public int ConnectorTypeId { get; set; }
        public int ConnectorId { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ModelEVSE ModelEVSE { get; set; }
        public virtual ConnectorType ConnectorType { get; set; }
        public virtual Capacity Capacity { get; set; }
    }
}
