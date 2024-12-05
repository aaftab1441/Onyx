using Abp.AutoMapper;

namespace Sixoclock.Onyx.RemoteStartStopEvents.Dto
{
    [AutoMapTo(typeof(RemoteStartStopEvent))]
    public class CreateOrUpdateRemoteStartStopEventInput
    {
        public int Id { get; set;}
        public int RemoteStartStopEventTypeId { get; set; }
        public int EVSEId { get; set; }
        public int TenantId { get; set; }
        public int RemoteStartStopStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
    }
}
