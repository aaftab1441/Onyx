using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class Context : FullAuditedEntity, IMustHaveTenant
    {
        public string ContextName { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<MeterValue> MeterValues { get; set; }
    }
}
