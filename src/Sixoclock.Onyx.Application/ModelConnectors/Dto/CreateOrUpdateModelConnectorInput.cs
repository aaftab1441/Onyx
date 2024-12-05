using Abp.AutoMapper;
using System.Collections.Generic;

namespace Sixoclock.Onyx.ModelConnectors.Dto
{
    [AutoMapTo(typeof(ModelConnector))]
    public class CreateOrUpdateModelConnectorInput
    {
        public int Id { get; set; }
        public int ModelEVSEId { get; set; }
        public int CapacityId { get; set; }
        public int ConnectorTypeId { get; set; }
        public int ConnectorId { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public List<ModelFeature> ModelFeatures { get; set; }
    }
}
