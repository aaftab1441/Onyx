using System.Collections.Generic;

namespace Sixoclock.Onyx.AvailabilityTypes.Dto
{
    public class GetAvailabilityTypesListOutput
    {
        public IEnumerable<AvailabilityTypeListDto> AvailabilityTypes { get; set; }
    }
}
