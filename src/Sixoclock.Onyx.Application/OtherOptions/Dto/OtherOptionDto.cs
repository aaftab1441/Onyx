using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.OtherOptions.Dto
{
    [AutoMapFrom(typeof(OtherOption))]
    public class OtherOptionDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
    }
}
