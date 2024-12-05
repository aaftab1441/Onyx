using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class TagTransaction : FullAuditedEntity, IMustHaveTenant
    {
        public DateTime TagTransactioTime { get; set; }

        public int TagId { get; set; }
        public int TagTransactionTypeId { get; set; }
        public int TransactionId { get; set; }
        public int TenantId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual TagTransactionType TagTransactionType { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
