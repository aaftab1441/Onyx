using Abp.AutoMapper;

namespace Sixoclock.Onyx.OCPPTransports.Dto
{
    [AutoMapTo(typeof(OCPPTransport))]
    public class CreateOrUpdateOCPPTransportInput
    {
        public int Id { get; set; }
        public string OCPPTransportName { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
    }
}
