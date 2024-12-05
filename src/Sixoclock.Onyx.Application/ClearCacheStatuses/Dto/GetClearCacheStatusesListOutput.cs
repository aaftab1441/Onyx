using System.Collections.Generic;

namespace Sixoclock.Onyx.ClearCacheStatuses.Dto
{
    public class GetClearCacheStatusesListOutput
    {
        public IEnumerable<ClearCacheStatusListDto> ClearCacheStatuses { get; set; }
    }
}
