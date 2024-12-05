using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Group))]
    public class Group : FullAuditedEntity, IMustHaveTenant
    {
        [Ruleable(typeof(string), "Name")]
        public string GroupName { get; set; }
        public string Comment { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public int InstallId { get; set; }
        public int? CountryId { get; set; }
        public int TenantId { get; set; }
        public virtual Install Install { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Chargepoint> Chargepoints { get; set; }
    }
}
