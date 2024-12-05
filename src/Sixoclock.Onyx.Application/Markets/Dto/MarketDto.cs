using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Markets.Dto
{
    [AutoMapFrom(typeof(Market))]
    public class MarketDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string MarketName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
