﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class MeterValueType : FullAuditedEntity, IMustHaveTenant
    {
        public string Type { get; set; }
        public string Comment { get; set; }

        public int TenantId { get; set; }

        public virtual ICollection<MeterValue> MeterValues { get; set; }
    }
}
