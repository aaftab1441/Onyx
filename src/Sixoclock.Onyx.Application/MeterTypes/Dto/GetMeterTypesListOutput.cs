using System.Collections.Generic;

namespace Sixoclock.Onyx.MeterTypes.Dto
{
    public class GetMeterTypesListOutput
    {
        public IEnumerable<MeterTypeListDto> MeterTypes { get; set; }
    }
}
