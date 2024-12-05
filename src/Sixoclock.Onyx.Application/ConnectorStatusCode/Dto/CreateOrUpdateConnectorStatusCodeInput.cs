using Abp.AutoMapper;

namespace Sixoclock.Onyx.ConnectorStatusCodes.Dto
{
    [AutoMapTo(typeof(ConnectorStatusCode))]
    public class CreateOrUpdateConnectorStatusCodeInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
