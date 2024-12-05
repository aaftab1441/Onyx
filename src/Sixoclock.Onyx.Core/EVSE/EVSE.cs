using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(EVSE))]
    public class EVSE : FullAuditedEntity, IMustHaveTenant
    {
        public int ChargepointId { get; set; }
        public int? MeterTypeId { get; set; }
        public int? AvailabilityTypeId { get; set; }
        public int? EVSEStatusId { get; set; }
        [Ruleable(typeof(string))]
        public string Comment { get; set; }
        public int EVSE_id { get; set; }
        public int TenantId { get; set; }
        
        public virtual Chargepoint Chargepoint { get; set; }
        public virtual MeterType MeterType { get; set; }
        public virtual AvailabilityType AvailabilityType { get; set; }
        public virtual EVSEStatus EVSEStatus { get; set; }
        public virtual ICollection<Connector> Connectors { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<RemoteStartStopEvent> RemoteStartStopEvents { get; set; }
    }
}
