using System.Collections.Generic;

namespace Sixoclock.Onyx.Regions.Dto
{
    public class GetRegionsListOutput
    {
        public IEnumerable<RegionListDto> Regions { get; set; }
    }
}
