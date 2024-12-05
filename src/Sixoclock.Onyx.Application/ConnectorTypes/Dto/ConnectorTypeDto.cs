using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.ConnectorTypes.Dto
{
    [AutoMapFrom(typeof(ConnectorType))]
    public class ConnectorTypeDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public string ConnectorName { get; set; }
        public string Comment { get; set; }
    }
}
