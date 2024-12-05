using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.MeterValues.Dto;
using System;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Transactions.Dto
{
    [AutoMapFrom(typeof(Transaction))]
    public class TransactionDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public DateTime? TransactionStartTime { get; set; }
        public DateTime? TransactionStopTime { get; set; }
        public string Comment { get; set; }
        public int? ReasonId { get; set; }
        public int EVSEId { get; set; }
        public int TransactionStatusId { get; set; }
        public int? TransactionTypeId { get; set; }
        public string ClientName { get; internal set; }
        public string MarketName { get; internal set; }
        public string RegionName { get; internal set; }
        public string InstallName { get; internal set; }
        public string GroupName { get; internal set; }
        public string VendorName { get; internal set; }
        public string ModelName { get; internal set; }
        public string Place { get; internal set; }
        public string Capacity { get; internal set; }
        public string PowerName { get; internal set; }
        public string ConnectorName { get; internal set; }
        public string TransactionType { get; internal set; }
        public string TransactionStatusValue { get; internal set; }
        public string Identity { get; internal set; }
        public string RemoteReason { get; internal set; }
        public int StartTagId { get; internal set; }
        public int ConnectorNumber { get; internal set; }
        public string TransactionStartUserName { get; internal set; }
        public string TransactionStopUserName { get; internal set; }
        public string MeterType { get; internal set; }
        public ListResultDto<MeterValueDto> ConnectorMeterValues { get; set; }
        public ListResultDto<TagTransactionDto> TagDetails { get; internal set; }
        public int EVSE_id { get; internal set; }
        public int ClientId { get; internal set; }
        public int MarketId { get; internal set; }
        public int RegionId { get; internal set; }
        public int InstallId { get; internal set; }
        public int GroupId { get; internal set; }
        public int ChargepointId { get; internal set; }
        public int VendorId { get; internal set; }
        public int ChargepointModelId { get; internal set; }
        public DateTime? Duration { get; internal set; }
        public float Kwh { get; internal set; }
        public string Tag { get; internal set; }
        public float? Cost { get; internal set; }
    }
}
