using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ChargepointServicePriceParameter:FullAuditedEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int ChargepointServiceId { get; set; }
        public ChargepointService ChargepointService { get; set; }
    }
}
