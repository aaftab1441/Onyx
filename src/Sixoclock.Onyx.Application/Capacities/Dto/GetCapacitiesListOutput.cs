using System.Collections.Generic;

namespace Sixoclock.Onyx.Capacities.Dto
{
    public class GetCapacitiesListOutput
    {
        public IEnumerable<CapacityListDto> Capacities { get; set; }
    }
}
