using System.Collections.Generic;

namespace Sixoclock.Onyx.ChargePointModels.Dto
{
    public class GetChargepointModelsListOutput
    {
        public IEnumerable<ChargepointModelListDto> ChargepointModels { get; set; }
    }
}
