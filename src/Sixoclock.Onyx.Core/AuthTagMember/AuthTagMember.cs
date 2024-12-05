using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class AuthTagMember : FullAuditedEntity, IMustHaveTenant
    {
        public int TagId { get; set; }
        public int LocalAuthListId { get; set; }
        public int TenantId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual LocalAuthList LocalAuthList { get; set; }
    }
}
