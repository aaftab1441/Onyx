﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class OtherOption : FullAuditedEntity, IMustHaveTenant
    {
        public string Option { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<OtherOptionModel> OtherOptionModels { get; set; }
    }
}
