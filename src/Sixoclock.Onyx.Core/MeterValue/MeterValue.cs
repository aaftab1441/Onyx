using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx
{
    public class MeterValue : FullAuditedEntity, IMustHaveTenant
    {
        public int TransactionId { get; set; }
        public int? MeasurandId { get; set; }
        public int? ContextId { get; set; }
        public int? LocationId { get; set; }
        public int? UnitId { get; set; }
        public int? PhaseId { get; set; }
        public int? FormatId { get; set; }
        public int MeterValueTypeId { get; set; }
        public float Value { get; set; }
        public DateTime MeterTime { get; set; }
        public int TenantId { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual Measurand Measurand { get; set; }
        public virtual Context Context { get; set; }
        public virtual Location Location { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Phase Phase { get; set; }
        public virtual Format Format { get; set; }
        public virtual MeterValueType MeterValueType { get; set; }
    }
}
