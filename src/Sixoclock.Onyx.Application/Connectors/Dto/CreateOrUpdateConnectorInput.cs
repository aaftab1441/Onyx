using Abp.AutoMapper;

namespace Sixoclock.Onyx.Connectors.Dto
{
    [AutoMapTo(typeof(Connector))]
    public class CreateOrUpdateConnectorInput
    {
        public int Id { get; set; }
        public int EVSEId { get; set; }
        public int ConnectorTypeId { get; set; }
        public int? UnlockStatusId { get; set; }
        public int CapacityId { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
    }
}
