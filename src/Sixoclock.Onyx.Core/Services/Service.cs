using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class Service:FullAuditedEntity
    {
        public string Name { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ServicePriceParameter> ServicePriceParameters { get; set; }
        public virtual ICollection<SegmentService> SegmentServices { get; set; }
    }
}
