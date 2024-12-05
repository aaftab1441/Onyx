using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.ClearCacheEvents.Dto
{
    [AutoMapFrom(typeof(ClearCacheEvent))]
    public class ClearCacheEventDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int ChargepointId { get; set; }
        public int ClearCacheStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public DateTime Date { get; set; }
        public int TenantId { get; set; }
        public string ClearCacheStatusValue { get; internal set; }
        public string MessageEventResponse { get; internal set; }
    }
}
