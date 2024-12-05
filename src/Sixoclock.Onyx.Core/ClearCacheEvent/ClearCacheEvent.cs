using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ClearCacheEvent:FullAuditedEntity,IMustHaveTenant
    {
        public int ChargepointId { get; set; }
        public int ClearCacheStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public DateTime Date { get; set; }
        public int TenantId { get; set; }
        public virtual Chargepoint Connector { get; set; }
        public virtual ClearCacheStatus ClearCacheStatus { get; set; }
        public virtual OCPPMessageEvent OcppMessageEvent { get; set; }
    }
}
