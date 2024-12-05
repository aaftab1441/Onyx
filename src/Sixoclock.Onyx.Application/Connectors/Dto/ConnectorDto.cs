using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Connectors.Dto
{
    [AutoMapFrom(typeof(Connector))]
    public class ConnectorDto : FullAuditedEntity, IHasCreationTime
    {
        public int EVSEId { get; set; }
        public int ConnectorTypeId { get; set; }
        public int? UnlockStatusId { get; set; }
        public int CapacityId { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
        public string ConnectorType { get; internal set; }
        public string ModelName { get; internal set; }
        public string Vendor { get; internal set; }
        public int ChargepointId { get; internal set; }
    }
}
