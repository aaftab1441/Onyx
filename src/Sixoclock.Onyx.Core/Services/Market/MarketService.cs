using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class MarketService:FullAuditedEntity
    {
        public int ServiceId { get; set; }
        public int MarketId { get; set; }
        public Market Segment { get; set; }
        public Service Service { get; set; }
        public virtual ICollection<MarketServicePriceParameter> MarketServicePriceParameters { get; set; }
    }
}
