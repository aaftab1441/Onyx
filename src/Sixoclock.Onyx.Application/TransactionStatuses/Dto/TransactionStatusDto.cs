using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.TransactionStatuses.Dto
{
    [AutoMapFrom(typeof(TransactionStatus))]
    public class TransactionStatusDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
    }
}
