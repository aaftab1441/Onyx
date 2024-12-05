using System.Collections.Generic;

namespace Sixoclock.Onyx.ResetEvents.Dto
{
    public class GetResetEventsListOutput
    {
        public IEnumerable<ResetEventListDto> ResetEvents { get; set; }
    }
}
