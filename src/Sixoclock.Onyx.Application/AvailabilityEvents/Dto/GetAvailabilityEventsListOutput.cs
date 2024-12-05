using System.Collections.Generic;

namespace Sixoclock.Onyx.AvailabilityEvents.Dto
{
    public class GetAvailabilityEventsListOutput
    {
        public IEnumerable<AvailabilityEventListDto> AvailabilityEvents { get; set; }
    }
}
