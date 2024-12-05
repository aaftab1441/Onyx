using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Sixoclock.Onyx
{
    public class ChargepointModel : FullAuditedEntity, IMustHaveTenant
    {
        public string ModelName { get; set; }
        public string Comment { get; set; }
        public int MountTypeId { get; set; }
        public int VendorId { get; set; }
        public string FirmwareLocation { get; set; }
        public int OCPPVersionId { get; set; }
        public int OCPPTransportId { get; set; }

        public int TenantId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual MountType MountType { get; set; }

        public virtual OCPPVersion OCPPVersion { get; set; }
        public virtual OCPPTransport OCPPTransport { get; set; }

        public virtual ICollection<ReleaseOptionModel> ReleaseOptionModels { get; set; }
        public virtual ICollection<ComOptionModel> ComOptionModels { get; set; }
        public virtual ICollection<ElectricalOptionModel> ElectricalOptionModels { get; set; }
        public virtual ICollection<ChargepointModelImage> ChargepointModelImages { get; set; }
        public virtual ICollection<LangDescription> LangDescriptions { get; set; }

        public virtual ICollection<ModelFeature> ModelFeatures { get; set; }
        public virtual ICollection<ModelKeyValue> ModelKeyValues { get; set; }
        public virtual ICollection<ModelEVSE> ModelEVSEs { get; set; }
        public virtual ICollection<Chargepoint> Chargepoints { get; set; }
    }
}
