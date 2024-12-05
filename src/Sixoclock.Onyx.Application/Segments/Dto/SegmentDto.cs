using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Segments.Dto
{
    [AutoMapFrom(typeof(Segment))]
    public class segmentListDtos : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
