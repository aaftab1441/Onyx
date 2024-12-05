using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.ComOptions.Dto
{
    [AutoMapFrom(typeof(ComOption))]
    public class ComOptionDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
    }
}
