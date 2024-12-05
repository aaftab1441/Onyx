using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class RegionService:FullAuditedEntity
    {
        public int ServiceId { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public Service Service { get; set; }
        public virtual ICollection<RegionServicePriceParameter> RegionServicePriceParameters { get; set; }
    }
}
