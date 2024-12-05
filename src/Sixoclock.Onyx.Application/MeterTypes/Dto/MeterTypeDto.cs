using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.MeterTypes.Dto
{
    [AutoMapFrom(typeof(MeterType))]
    public class MeterTypeDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
    }
}
