using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class TagTransactionType : FullAuditedEntity, IMustHaveTenant
    {
        public string Value { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<TagTransaction> TagTransactions { get; set; }
    }
}
