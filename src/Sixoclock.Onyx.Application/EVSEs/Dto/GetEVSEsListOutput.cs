using System.Collections.Generic;

namespace Sixoclock.Onyx.EVSEs.Dto
{
    public class GetEVSEsListOutput
    {
        public IEnumerable<EVSEListDto> EVSEs { get; set; }
    }
}
