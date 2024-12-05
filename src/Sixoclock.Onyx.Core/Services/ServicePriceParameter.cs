using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class ServicePriceParameter:FullAuditedEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }

    }
}
