using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class OCPPMessage : FullAuditedEntity, IMustHaveTenant
    {
        public string Message { get; set; }
        public string Comment { get; set; }
        public int OCPPVersionId { get; set; }
        public int TenantId { get; set; }

        public virtual OCPPVersion OCPPVersion { get; set; }
    }
}
