using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Transaction))]
    public class Transaction : FullAuditedEntity, IMustHaveTenant
    {
        public int? ReasonId { get; set; }
        public int TransactionStatusId { get; set; }
        public int? TransactionTypeId { get; set; }
        [Ruleable(typeof(TimeSpan),"Start Time")]
        public DateTime? TransactionStartTime { get; set; }
        public DateTime? TransactionStopTime { get; set; }
        [Ruleable(typeof(TimeSpan))]
        public DateTime? Duration { get; set; }
        [Ruleable(typeof(float))]
        public float? Cost { get; set; }
        [Ruleable(typeof(float))]
        public float? Earned { get; set; }
        [Ruleable(typeof(float),"To Billed")]
        public float? ToBilled { get; set; }
        public string Comment { get; set; }
        public int EVSEId { get; set; }
        public float KwhDelivered { get; set; }
        public int CO2Saved { get; set; }
        public int? CurrencyId { get; set; }
        public int TenantId { get; set; }
        [Ruleable(typeof(Reason))]
        public virtual Reason Reason { get; set; }
        [Ruleable(typeof(TransactionStatus),"Transaction Status")]
        public virtual TransactionStatus TransactionStatus { get; set; }
        [Ruleable(typeof(TransactionType),"Transaction Type")]
        public virtual TransactionType TransactionType { get; set; }
        public virtual EVSE EVSE { get; set; }
        public virtual ICollection<TagTransaction> TagTransactions { get; set; }
        public virtual ICollection<MeterValue> MeterValues { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
