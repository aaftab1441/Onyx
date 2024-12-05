using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class ConnectorType : FullAuditedEntity, IMustHaveTenant
    {
        public string ConnectorName { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
        
        public virtual ICollection<ModelConnector> ModelConnectors { get; set; }
        public virtual ICollection<Connector> Connectors { get; set; }
    }
}
