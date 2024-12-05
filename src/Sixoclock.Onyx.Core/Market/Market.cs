using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Market))]
    public class Market : FullAuditedEntity, IMustHaveTenant
    {
        [Ruleable(typeof(string), "Name")]
        public string MarketName { get; set; }

        public string Comment { get; set; }

        public int CustomerId { get; set; }

        public int TenantId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
