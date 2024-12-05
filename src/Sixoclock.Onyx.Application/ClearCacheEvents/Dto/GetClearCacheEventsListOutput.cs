using System.Collections.Generic;

namespace Sixoclock.Onyx.ClearCacheEvents.Dto
{
    public class GetClearCacheEventsListOutput
    {
        public IEnumerable<ClearCacheEventListDto> ClearCacheEvents { get; set; }
    }
}
