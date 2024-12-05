using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class RemoteStartStopEventType : FullAuditedEntity, IMustHaveTenant
    {
        public string EventType { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<RemoteStartStopEvent> RemoteStartStopEvents { get; set; }
    }
}
