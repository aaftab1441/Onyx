using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx
{
    public class ResetEvent : FullAuditedEntity, IMustHaveTenant
    {
        public int ChargepointId { get; set; }
        public int ResetTypeId { get; set; }
        public int ResetStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public DateTime Date { get; set; }

        public int TenantId { get; set; }

        public virtual ResetStatus ResetStatus { get; set; }
        public virtual Chargepoint Connector { get; set; }
        public virtual ResetType ResetType { get; set; }
        public virtual OCPPMessageEvent OcppMessageEvent { get; set; }
    }
}
