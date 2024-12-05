using System.Collections.Generic;

namespace Sixoclock.Onyx.ModelFeatures.Dto
{
    public class GetModelFeaturesListOutput
    {
        public IEnumerable<ModelFeatureListDto> ModelFeatures { get; set; }
    }
}
