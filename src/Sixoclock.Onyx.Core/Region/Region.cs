using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Region))]
    public class Region : FullAuditedEntity, IMustHaveTenant
    {
        [Ruleable(typeof(string), "Name")]
        public string RegionName { get; set; }
        public string Comment { get; set; }
        public int MarketId { get; set; }

        public int TenantId { get; set; }

        public virtual Market Market { get; set; }

        public virtual ICollection<Install> Installations { get; set; }
    }
}
