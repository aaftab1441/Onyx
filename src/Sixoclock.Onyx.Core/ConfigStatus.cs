﻿using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ConfigStatus:FullAuditedEntity,IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Value { get; set; }
        public virtual ICollection<ConfigEvent> ConfigEvents { get; set; }
    }
}
