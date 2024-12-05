using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class SegmentService:FullAuditedEntity
    {
        public int ServiceId { get; set; }
        public int SegmentId { get; set; }
        public Segment Segment { get; set; }
        public Service Service { get; set; }
        public virtual ICollection<SegmentServicePriceParameter> SegmentServicePriceParameters { get; set; }


    }
}
