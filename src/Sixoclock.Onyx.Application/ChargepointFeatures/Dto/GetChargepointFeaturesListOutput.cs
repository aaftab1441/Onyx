using System.Collections.Generic;

namespace Sixoclock.Onyx.ChargepointFeatures.Dto
{
    public class GetChargepointFeaturesListOutput
    {
        public IEnumerable<ChargepointFeatureListDto> ChargepointFeatures { get; set; }
    }
}
