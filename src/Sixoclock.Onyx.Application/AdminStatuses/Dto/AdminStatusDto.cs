using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.AdminStatuses.Dto
{
    [AutoMapFrom(typeof(AdminStatus))]
    public class AdminStatusDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
