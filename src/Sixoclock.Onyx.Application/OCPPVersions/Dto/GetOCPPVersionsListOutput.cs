using System.Collections.Generic;

namespace Sixoclock.Onyx.OCPPVersions.Dto
{
    public class GetOCPPVersionsListOutput
    {
        public IEnumerable<OCPPVersionListDto> OCPPVersions { get; set; }
    }
}
