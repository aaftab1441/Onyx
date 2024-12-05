using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class ModelEVSE : FullAuditedEntity, IMustHaveTenant
    {
        public int? MeterTypeId { get; set; }
        public int ChargepointModelId { get; set; }
        public string Comment { get; set; }
        public int EVSEId { get; set; }
        public int TenantId { get; set; }

        public virtual MeterType MeterType { get; set; }
        public virtual ChargepointModel ChargepointModel { get; set; }
        public virtual ICollection<ModelConnector> ModelConnectors { get; set; }
    }
}
