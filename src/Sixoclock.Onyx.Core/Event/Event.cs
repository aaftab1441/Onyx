using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx
{
    public class Event : FullAuditedEntity, IMustHaveTenant
    {
        public string EventValue { get; set; }
        public DateTime EventTime { get; set; }
        public int ChargepointId { get; set; }
        public int ConnectorEventTypeId { get; set; }
        public int TenantId { get; set; }

        public virtual Chargepoint Connector { get; set; }
        public virtual EventType EventTypes { get; set; }
    }
}
