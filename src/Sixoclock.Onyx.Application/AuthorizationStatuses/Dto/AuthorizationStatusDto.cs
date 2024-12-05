using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.AuthorizationStatuses.Dto
{
    [AutoMapFrom(typeof(AuthorizationStatus))]
    public class AuthorizationStatusDto : FullAuditedEntityDto, IHasCreationTime
    {
        public string Value { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
    }
}
