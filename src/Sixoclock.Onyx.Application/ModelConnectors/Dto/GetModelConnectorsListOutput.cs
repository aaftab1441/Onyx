using System.Collections.Generic;

namespace Sixoclock.Onyx.ModelConnectors.Dto
{
    public class GetModelConnectorsListOutput
    {
        public IEnumerable<ModelConnectorListDto> ModelConnectors { get; set; }
    }
}
