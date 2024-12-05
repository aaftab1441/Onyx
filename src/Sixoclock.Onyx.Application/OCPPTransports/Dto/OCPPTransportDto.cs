using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.OCPPTransports.Dto
{
    [AutoMapFrom(typeof(OCPPTransport))]
    public class OCPPTransportDto : FullAuditedEntityDto
    {
        public string OCPPTransportName { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
    }
}
