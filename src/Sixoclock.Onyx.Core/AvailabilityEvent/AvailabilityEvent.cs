using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class AvailabilityEvent:FullAuditedEntity,IMustHaveTenant
    {
        public int AvailabilityStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public int AvailabilityTypeId { get; set; }
        public int EVSEId { get; set; }
        public int TenantId { get; set; }
        public DateTime Date { get; set; }
        public virtual AvailabilityStatus AvailabilityStatus { get; set; }
        public virtual OCPPMessageEvent OcppMessageEvent { get; set; }
        public virtual AvailabilityType AvailabilityType { get; set; }
        public virtual EVSE EVSE { get; set; }
    }
}
