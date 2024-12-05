using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class GroupService:FullAuditedEntity
    {
        public int ServiceId { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public Service Service { get; set; }
        public virtual ICollection<GroupServicePriceParameter> GroupServicePriceParameters { get; set; }
    }
}
