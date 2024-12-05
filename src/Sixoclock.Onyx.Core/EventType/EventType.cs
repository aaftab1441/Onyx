using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class EventType : FullAuditedEntity, IMustHaveTenant
    {
        public string Event { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
