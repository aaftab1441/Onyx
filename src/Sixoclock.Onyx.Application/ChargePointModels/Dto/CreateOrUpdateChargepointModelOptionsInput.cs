using System.Collections.Generic;

namespace Sixoclock.Onyx.ChargePointModels.Dto
{
    public class CreateOrUpdateChargepointModelOptionsInput
    {
        public List<ReleaseOptionModel> ReleaseOptionModels { get; set; }
        public List<ElectricalOptionModel> ElectricalOptionModels { get; set; }
        public List<ComOptionModel> ComOptionModels { get; set; }
        public List<OtherOptionModel> OtherOptionModels { get; set; }
    }
}
