using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ConnectorStatusCodes.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ConnectorStatusCodes
{
    public interface IConnectorStatusCodeAppService : IApplicationService
    {
        Task CreateOrUpdateConnectorStatusCode(CreateOrUpdateConnectorStatusCodeInput input);
        Task DeleteConnectorStatusCode(EntityDto<int> input);
        Task<PagedResultDto<ConnectorStatusCodeDto>> GetConnectorStatusCode(GetConnectorStatusCodeInput input);
        Task<GetConnectorStatusCodeForEditOutput> GetConnectorStatusCodeForEdit(EntityDto<int> input);
    }
}