using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class MarketServicePriceParameter:FullAuditedEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int MarketServiceId { get; set; }
        public MarketService MarketService { get; set; }
    }
}
