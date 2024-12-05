using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.ConnectorStatusCodes.Dto
{
    [AutoMapFrom(typeof(ConnectorStatusCode))]
    public class ConnectorStatusCodeDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
