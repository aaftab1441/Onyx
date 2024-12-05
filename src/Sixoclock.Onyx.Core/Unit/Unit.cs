using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class Unit : FullAuditedEntity, IMustHaveTenant
    {
        public string UnitName { get;set;}
        public string Comment { get; set; }

        public int TenantId { get; set; }

        public virtual ICollection<Capacity> Capacities { get; set; }
    }
}
