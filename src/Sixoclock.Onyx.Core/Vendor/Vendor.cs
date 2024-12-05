﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class Vendor : FullAuditedEntity, IMustHaveTenant
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<ChargepointModel> ChargepointModels { get; set; }
        public virtual ICollection<VendorErrorCode> VendorErrorCodes { get; set; }
    }
}
