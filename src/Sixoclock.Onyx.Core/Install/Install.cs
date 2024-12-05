using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Install))]
    public class Install : FullAuditedEntity, IMustHaveTenant
    {
        [Ruleable(typeof(string), "Name")]
        public string InstallName { get; set; }
        public string Comment { get; set; }
        public int RegionId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int TenantId { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
