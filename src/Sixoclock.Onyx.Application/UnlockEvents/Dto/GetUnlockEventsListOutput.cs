using System.Collections.Generic;

namespace Sixoclock.Onyx.UnlockEvents.Dto
{
    public class GetUnlockEventsListOutput
    {
        public IEnumerable<UnlockEventListDto> UnlockEvents { get; set; }
    }
}
