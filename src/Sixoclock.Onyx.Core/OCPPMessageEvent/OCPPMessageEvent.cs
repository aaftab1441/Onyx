using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class OCPPMessageEvent : FullAuditedEntity, IMustHaveTenant
    {
        public string UniqueId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public int OCPPStatusId { get; set; }
        public int ChargepointId { get; set; }
        public int TenantId { get; set; }
        public virtual OCPPStatus OCPPStatus { get; set; }
        public virtual Chargepoint Connector { get; set; }
        
    }
}
