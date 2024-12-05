using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class UnlockEvent:FullAuditedEntity,IMustHaveTenant
    {
        public int EVSEId { get; set; }
        public int? UnlockStatusId { get; set; }
        public int TenantId { get; set; }
        public DateTime Date { get; set; }
        public int? OcppMessageEventId { get; set; }
        public virtual EVSE EVSE { get; set; }
        public virtual UnlockStatus UnlockStatus { get; set; }
        public virtual OCPPMessageEvent OcppMessageEvent { get; set; }
    }
}
