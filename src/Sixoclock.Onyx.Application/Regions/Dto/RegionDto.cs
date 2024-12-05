using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Regions.Dto
{
    [AutoMapFrom(typeof(Region))]
    public class RegionDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string RegionName { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
    }
}
