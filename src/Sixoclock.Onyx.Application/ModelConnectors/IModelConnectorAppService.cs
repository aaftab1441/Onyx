using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ModelConnectors.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ModelConnectors
{
    public interface IModelConnectorAppService : IApplicationService
    {
        Task CreateOrUpdateModelConnector(CreateOrUpdateModelConnectorInput input);
        Task DeleteModelConnector(EntityDto<int> input);
        Task<PagedResultDto<ModelConnectorDto>> GetModelConnector(GetModelConnectorInput input);
        GetModelConnectorsListOutput GetModelConnectorsList();
        GetModelConnectorsByChargepointModelListOutput GetModelConnectorsByModelEVSEList(int modeEVSElId);
        GetModelConnectorForEditOutput GetModelConnectorForEdit(EntityDto<int> input);
        int GetConnectorNo(EntityDto<int> input);
        string GetCapacitiesByModelId(EntityDto<int> input);
    }
}