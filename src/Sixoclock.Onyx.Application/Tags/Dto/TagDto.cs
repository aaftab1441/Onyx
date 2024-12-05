using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.MeterValues.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;
using System;

namespace Sixoclock.Onyx.Tags.Dto
{
    [AutoMapFrom(typeof(Tag))]
    public class TagDto : FullAuditedEntityDto, IHasCreationTime
    {
        public string IdToken { get; set; }
        public DateTime? Expiry { get; set; }
        public bool? ServiceContact { get; set; }
        public string Comment { get; set; }
        public int ParentTagId { get; set; }
        public int? AuthorizationStatusId { get; set; }
        public int TenantId { get; set; }
        public long? UserId { get; set; }
        public int CustomerId { get; set; }
        public int MarketId { get; set; }
        public int RegionId { get; set; }
        public int InstallId { get; set; }
        public int GroupId { get; set; }
        public string AuthorizationStatus { get; internal set; }
        public string UserName { get; internal set; }

        public ListResultDto<MeterValueDto> ConnectorMeterValues { get; set; }
        public ListResultDto<TagTransactionDto> TagDetails { get; internal set; }
        public object ClientName { get; internal set; }
        public object MarketName { get; internal set; }
        public string MeterType { get; internal set; }
        public string RemoteReason { get; internal set; }
        public string Identity { get; internal set; }
        public string TransactionType { get; internal set; }
        public int? TransactionTypeId { get; internal set; }
        public string TransactionStatusValue { get; internal set; }
        public int TransactionStatusId { get; internal set; }
        public int EVSEId { get; internal set; }
        public int? ReasonId { get; internal set; }
        public DateTime? TransactionStopTime { get; internal set; }
        public DateTime? TransactionStartTime { get; internal set; }
        public string Place { get; internal set; }
        public string ModelName { get; internal set; }
        public string VendorName { get; internal set; }
        public string GroupName { get; internal set; }
        public string InstallName { get; internal set; }
        public string RegionName { get; internal set; }
        public string TransactionStartUserName { get; internal set; }
        public string TransactionStopUserName { get; internal set; }
    }
}
