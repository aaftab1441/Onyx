using System.Collections.Generic;

namespace Sixoclock.Onyx.MeterValues.Dto
{
    public class GetMeterValuesListOutput
    {
        public IEnumerable<MeterValueListDto> MeterValues { get; set; }
    }
}
