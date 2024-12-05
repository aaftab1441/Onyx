using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ChargepointService:FullAuditedEntity
    {
        public int ServiceId { get; set; }
        public int ChargepointId { get; set; }
        public Chargepoint Chargepoint { get; set; }
        public Service Service { get; set; }
        public virtual ICollection<ChargepointServicePriceParameter> ChargepointServicePriceParameters { get; set; }
    }
}
