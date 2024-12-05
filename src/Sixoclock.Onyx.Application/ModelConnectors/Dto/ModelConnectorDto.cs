using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.ModelConnectors.Dto
{
    [AutoMapFrom(typeof(ModelConnector))]
    public class ModelConnectorDto : FullAuditedEntityDto
    {
        public int ModelEVSEId { get; set; }
        public int CapacityId { get; set; }
        public int ConnectorTypeId { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
        public string ConnectorType { get; internal set; }
        public string ModelName { get; internal set; }
        public string Vendor { get; internal set; }
        public int VendorId { get; set; }
        public int ChargepointModelId { get; internal set; }
        public int ConnectorId { get; internal set; }
    }
}
