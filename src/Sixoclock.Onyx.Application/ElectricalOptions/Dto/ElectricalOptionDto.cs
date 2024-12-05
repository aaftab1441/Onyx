using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.ElectricalOptions.Dto
{
    [AutoMapFrom(typeof(ElectricalOption))]
    public class ElectricalOptionDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
    }
}
