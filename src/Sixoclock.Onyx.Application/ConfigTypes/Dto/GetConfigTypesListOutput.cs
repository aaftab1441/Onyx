using System.Collections.Generic;

namespace Sixoclock.Onyx.ConfigTypes.Dto
{
    public class GetConfigTypesListOutput
    {
        public IEnumerable<ConfigTypeListDto> ConfigTypes { get; set; }
    }
}
