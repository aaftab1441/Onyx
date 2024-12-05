using System.Collections.Generic;

namespace Sixoclock.Onyx.OCPPFeatures.Dto
{
    public class GetOCPPFeaturesListOutput
    {
        public IEnumerable<OCPPFeatureListDto> OCPPFeatures { get; set; }
    }
}
