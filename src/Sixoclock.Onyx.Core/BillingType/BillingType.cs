using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.Authorization.Users;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class BillingType : FullAuditedEntity, IMustHaveTenant
    {
        public string Type { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
