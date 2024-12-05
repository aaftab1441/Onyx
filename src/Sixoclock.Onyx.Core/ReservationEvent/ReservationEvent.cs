using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class ReservationEvent : FullAuditedEntity, IMustHaveTenant
    {
        public DateTime EventTime { get; set; }

        public int ReservationEventTypeId { get; set; }
        public int ReservationId { get; set; }
        public int TenantId { get; set; }

        public virtual ReservationEventType ReservationEventType { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
