using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.OCPPTransports.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.OCPPTransports
{
    public interface IOCPPTransportAppService : IApplicationService
    {
        Task CreateOrUpdateOCPPTransport(CreateOrUpdateOCPPTransportInput input);
        Task DeleteOCPPTransport(EntityDto<int> input);
        ListResultDto<OCPPTransportDto> GetOCPPTransport(GetOCPPTransportInput input);
        Task<GetOCPPTransportForEditOutput> GetOCPPTransportForEdit(EntityDto<int> input);
        GetOCPPTransportsListOutput GetOCPPTransportsList();
    }
}