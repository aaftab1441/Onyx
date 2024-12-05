using System.Collections.Generic;

namespace Sixoclock.Onyx.Connectors.Dto
{
    public class GetConnectorsListOutput
    {
        public IEnumerable<ConnectorListDto> Connectors { get; set; }
    }
}
