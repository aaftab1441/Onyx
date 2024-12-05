using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class OCPPTransport : FullAuditedEntity, IMustHaveTenant
    {
        public string OCPPTransportName { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Chargepoint> Chargepoints { get; set; }
        public virtual ICollection<ChargepointModel> ChargepointModels { get; set; }
    }
}
