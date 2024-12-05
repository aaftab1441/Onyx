using System.Collections.Generic;

namespace Sixoclock.Onyx.ModelConnectors.Dto
{
    public class GetModelConnectorsByChargepointModelListOutput
    {
        public IEnumerable<ModelConnectorByChargepointModelListDto> ModelConnectors { get; set; }
    }
}
