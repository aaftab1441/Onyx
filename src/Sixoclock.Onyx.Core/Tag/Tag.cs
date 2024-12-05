using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Tag))]
    public class Tag : FullAuditedEntity, IMustHaveTenant
    {
        [Ruleable(typeof(string))]
        public string IdToken { get; set; }
        [Ruleable(typeof(DateTime))]
        public DateTime? Expiry { get; set; }
        public bool? ServiceContact { get; set; }
        public string Comment { get; set; }

        public int ParentTagId { get; set; }
        public int? AuthorizationStatusId { get; set; }
        public int? TagStatusId { get; set; }
        public long? UserId { get; set; }
        public int TenantId { get; set; }

        public virtual ParentTag ParentTag { get; set; }
        public virtual AuthorizationStatus AuthorizationStatus { get; set; }
        public virtual TagStatus TagStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AuthTagMember> AuthTagMembers { get; set; }
        public virtual ICollection<TagTransaction> TagTransactions { get; set; }
    }
}
