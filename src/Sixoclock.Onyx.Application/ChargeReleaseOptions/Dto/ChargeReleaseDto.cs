using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.ChargeReleaseOptions.Dto
{
    [AutoMapFrom(typeof(ChargeReleaseOption))]
    public class ChargeReleaseOptionDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
    }
}
