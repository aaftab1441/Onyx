using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class OCPPStatus : FullAuditedEntity, IMustHaveTenant
    {
        public string Status { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<OCPPMessageEvent> OCPPMessageEvents { get; set; }
    }
}
