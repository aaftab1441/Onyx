using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class Format : FullAuditedEntity, IMustHaveTenant
    {
        public string FormatType { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<MeterValue> MeterValues { get; set; }
    }
}
