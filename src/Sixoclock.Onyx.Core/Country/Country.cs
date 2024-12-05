using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.Authorization.Users;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{

    public class Country : FullAuditedEntity, IMustHaveTenant
    {
        public string CountryName { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
