using System.Collections.Generic;

namespace Sixoclock.Onyx.OCPPTransports.Dto
{
    public class GetOCPPTransportsListOutput
    {
        public IEnumerable<OCPPTransportListDto> OCPPTransports { get; set; }
    }
}
