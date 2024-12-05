using System.Collections.Generic;

namespace Sixoclock.Onyx.Chargepoints.Dto
{
    public class GetChargepointsListOutput
    {
        public IEnumerable<ChargepointListDto> Chargepoints { get; set; }
    }
}
