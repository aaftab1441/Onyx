using Abp.AutoMapper;

namespace Sixoclock.Onyx.ConnectorTypes.Dto
{
    [AutoMapTo(typeof(ConnectorType))]
    public class CreateOrUpdateConnectorTypeInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string ConnectorName { get; set; }
        public string Comment { get; set; }
    }
}
