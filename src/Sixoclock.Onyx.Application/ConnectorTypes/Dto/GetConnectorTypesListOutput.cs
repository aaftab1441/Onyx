using System.Collections.Generic;

namespace Sixoclock.Onyx.ConnectorTypes.Dto
{
    public class GetConnectorTypesListOutput
    {
        public IEnumerable<ConnectorTypeListDto> ConnectorTypes { get; set; }
    }
}
