using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx
{
    public class EVSEStatus : FullAuditedEntity, IMustHaveTenant
    {
        public int EVSEStatusCodeId { get; set; }
        public int? VendorErrorCodeId { get; set; }
        public int? ErrorCodeId { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
        public int TenantId { get; set; }

        public virtual ConnectorStatusCode EVSEStatusCode { get; set; }
        public virtual VendorErrorCode VendorErrorCode { get; set; }
        public virtual ErrorCode ErrorCode { get; set; }
        public virtual EVSE EVSE { get; set; }
    }
}
