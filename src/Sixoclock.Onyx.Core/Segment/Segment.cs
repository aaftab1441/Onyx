using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Segment))]
    public class Segment : FullAuditedEntity, IMustHaveTenant
    {
        [Ruleable(typeof(string))]
        public string Name { get; set; }
        
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Customer> Clients { get; set; }
        public virtual ICollection<SegmentService> SegmentServices { get; set; }
    }
}
