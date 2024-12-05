using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class LocalAuthList : FullAuditedEntity, IMustHaveTenant
    {
        public int UpdateStatusId { get; set; }
        public int UpdateTypeId { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<AuthTagMember> AuthTagMembers { get; set; }
        public virtual UpdateStatus UpdateStatus { get; set; }
        public virtual UpdateType UpdateType { get; set; }
    }
}
