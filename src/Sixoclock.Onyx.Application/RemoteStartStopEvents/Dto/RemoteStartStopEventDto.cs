using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.RemoteStartStopEvents.Dto
{
    [AutoMapFrom(typeof(RemoteStartStopEvent))]
    public class RemoteStartStopEventDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int RemoteStartStopEventTypeId { get; set; }
        public int EVSEId { get; set; }
        public int? TagId { get; set; }
        public int TenantId { get; set; }
        public int RemoteStartStopStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public string EventType { get; internal set; }
        public string StatusValue { get; internal set; }
        public string MessageEventResponse { get; internal set; }
        public int EVSE_id { get; internal set; }
    }
}
