using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class Reservation : FullAuditedEntity, IMustHaveTenant
    {
        public DateTime ExpiryDate { get; set; }

        public int ReservationStatusId { get; set; }
        public int CancelReservationStatusId { get; set; }
        public int EVSEId { get; set; }
        public int TenantId { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual CancelReservationStatus CancelReservationStatus { get; set; }
        public virtual EVSE EVSE { get; set; }
    }
}
