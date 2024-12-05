using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class VendorErrorCode : FullAuditedEntity, IMustHaveTenant
    {
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public string Comment { get; set; }
        public int VendorId { get; set; }
        public int TenantId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<EVSEStatus> EvseStatuses { get; set; }
    }
}
