using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.Authorization.Users;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Bill))]
    public class Bill:FullAuditedEntity,IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long UserId { get; set; }
        public int BillingStatusId { get; set; }
        public int BillingTypeId { get; set; }
        public int Transactions { get; set; }
        [Ruleable(typeof(string))]
        public string Number { get; set; }
        [Ruleable(typeof(int))]
        public int Totalkwh { get; set; }
        [Ruleable(typeof(DateTime))]
        public DateTime BillDate { get; set; }
        [Ruleable(typeof(DateTime))]
        public DateTime DueDate { get; set; }
        [Ruleable(typeof(string))]
        public string Comment { get; set; }
        public virtual User User { get; set; }
        [Ruleable(typeof(BillingStatus))]
        public virtual BillingStatus BillingStatus { get; set; }
        [Ruleable(typeof(BillingType))]
        public virtual BillingType BillingType { get; set; }
    }
}
