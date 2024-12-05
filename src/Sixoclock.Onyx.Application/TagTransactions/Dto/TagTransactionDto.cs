using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.TagTransactions.Dto
{
    [AutoMapFrom(typeof(TagTransaction))]
    public class TagTransactionDto : FullAuditedEntityDto, IHasCreationTime
    {
        public DateTime TagTransactioTime { get; set; }

        public int TagId { get; set; }
        public int TagTransactionTypeId { get; set; }
        public int TransactionId { get; set; }
        public int TenantId { get; set; }
        public string UserName { get; internal set; }
        public string TagTransactionType { get; internal set; }
        public string Parent { get; internal set; }
        public DateTime? Expiry { get; internal set; }
    }
}
