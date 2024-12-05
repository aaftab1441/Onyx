namespace Sixoclock.Onyx.ModelConnectors.Dto
{
    public class ModelConnectorByChargepointModelListDto
    {
        public int Id { get; set; }
        public int ModelEVSEId { get; set; }
        public int CapacityId { get; set; }
        public int ConnectorTypeId { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
    }
}
