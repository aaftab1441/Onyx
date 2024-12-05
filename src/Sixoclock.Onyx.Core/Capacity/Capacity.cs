using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class Capacity : FullAuditedEntity, IMustHaveTenant
    {
        public string Value { get; set; }
        public string Comment { get; set; }
        public int UnitId { get; set; }
        public int PowerId { get; set; }
        public int TenantId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual Power Power { get; set; }
        
        public virtual ICollection<ModelConnector> ModelConnectors { get; set; }
        public virtual ICollection<Connector> Connectors { get; set; }
    }
}
