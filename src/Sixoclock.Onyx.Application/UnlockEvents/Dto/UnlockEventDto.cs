using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.UnlockEvents.Dto
{
    [AutoMapFrom(typeof(UnlockEvent))]
    public class UnlockEventDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int EVSEId { get; set; }
        public int UnlockStatusId { get; set; }
        public int TenantId { get; set; }
        public DateTime Date { get; set; }
        public int? OcppMessageEventId { get; set; }
        public object Type { get; internal set; }
        public string MessageEventResponse { get; internal set; }
        public string UnlockStatusValue { get; internal set; }
        public int EVSE_Id { get; internal set; }
    }
}
