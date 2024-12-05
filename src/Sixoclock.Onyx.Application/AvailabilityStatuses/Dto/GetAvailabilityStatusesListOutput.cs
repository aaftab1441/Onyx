using System.Collections.Generic;

namespace Sixoclock.Onyx.AvailabilityStatuses.Dto
{
    public class GetAvailabilityStatusesListOutput
    {
        public IEnumerable<AvailabilityStatusListDto> AvailabilityStatuses { get; set; }
    }
}
