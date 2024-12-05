using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Capacities.Dto
{
    [AutoMapFrom(typeof(Capacity))]
    public class CapacityDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public int UnitId { get; set; }
        public int PowerId { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public string Power { get; set; }
        public string Unit { get; set; }
    }
}