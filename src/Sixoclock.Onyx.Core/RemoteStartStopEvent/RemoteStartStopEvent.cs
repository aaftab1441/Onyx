using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx
{
    public class RemoteStartStopEvent : FullAuditedEntity, IMustHaveTenant
    {
        public int RemoteStartStopEventTypeId { get; set; }
        public int EVSEId { get; set; }
        public int TenantId { get; set; }
        public int RemoteStartStopStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public virtual RemoteStartStopEventType RemoteStartStopEventType { get; set; }
        public virtual EVSE EVSE { get; set; }
        public virtual RemoteStartStopStatus RemoteStartStopStatus { get; set; }
        public virtual OCPPMessageEvent OcppMessageEvent { get; set; }
    }
}
