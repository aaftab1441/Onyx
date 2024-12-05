using System.Collections.Generic;

namespace Sixoclock.Onyx.UnlockStatuses.Dto
{
    public class GetUnlockStatusesListOutput
    {
        public IEnumerable<UnlockStatusListDto> UnlockStatuses { get; set; }
    }
}
