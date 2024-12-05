using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ConnectorTypes.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ConnectorTypes
{
    public interface IConnectorTypeAppService : IApplicationService
    {
        Task CreateOrUpdateConnectorType(CreateOrUpdateConnectorTypeInput input);
        Task DeleteConnectorType(EntityDto<int> input);
        ListResultDto<ConnectorTypeDto> GetConnectorType(GetConnectorTypeInput input);
        Task<GetConnectorTypeForEditOutput> GetConnectorTypeForEdit(EntityDto<int> input);
        GetConnectorTypesListOutput GetConnectorTypesList();
    }
}