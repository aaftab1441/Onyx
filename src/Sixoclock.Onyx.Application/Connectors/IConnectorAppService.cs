using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Connectors.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Connectors
{
    public interface IConnectorAppService : IApplicationService
    {
        Task CreateOrUpdateConnector(CreateOrUpdateConnectorInput input);
        Task DeleteConnector(EntityDto<int> input);
        Task<PagedResultDto<ConnectorDto>> GetConnector(GetConnectorInput input);
        GetConnectorForEditOutput GetConnectorForEdit(EntityDto<int> input);
        Task CopyModelConnectorToConnector(int modelEVSEId, int evseId);
    }
}