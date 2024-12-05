using Abp.AutoMapper;
using System.Collections.Generic;

namespace Sixoclock.Onyx.ChargePointModels.Dto
{
    [AutoMapTo(typeof(ChargepointModel))]
    public class CreateOrUpdateChargepointModelInput
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string Comment { get; set; }
        public int MountTypeId { get; set; }
        public int VendorId { get; set; }
        public string FirmwareLocation { get; set; }
        public int OCPPVersionId { get; set; }
        public int OCPPTransportId { get; set; }

        public int TenantId { get; set; }

        public List<ModelFeature> ModelFeatures { get; set; }
    }
}
