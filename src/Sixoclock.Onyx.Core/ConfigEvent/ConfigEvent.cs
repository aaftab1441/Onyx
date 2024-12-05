using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ConfigEvent:FullAuditedEntity,IMustHaveTenant
    {
        public int ChargepointId { get; set; }
        public int ConfigTypeId { get; set; }
        public int TenantId { get; set; }
        public int ConfigStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public DateTime Date { get; set; }
        public virtual ConfigType ConfigType { get; set; }
        public virtual Chargepoint Chargepoint { get; set; }
        public virtual ConfigStatus ConfigStatus { get; set; }
        public virtual OCPPMessageEvent OcppMessageEvent { get; set; }
    }
}
