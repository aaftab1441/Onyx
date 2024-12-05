using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ModelKeyValues.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ModelKeyValues
{
    public interface IModelKeyValueAppService : IApplicationService
    {
        Task CreateOrUpdateModelKeyValue(CreateOrUpdateModelKeyValueInput input);
        Task DeleteModelKeyValue(ModelKeyValueByChargepointModelIdListDto input);
        GetModelKeyValuesListByChargepointModelIdListOutput GetModelKeyValuesListByChargepointModelId(GetModelKeyValuesListByChargepointModelIdListInput input);
        Task<PagedResultDto<ModelKeyValueDto>> GetModelKeyValue(GetModelKeyValueInput input);
        Task<GetModelKeyValueForEditOutput> GetModelKeyValueForEdit(EntityDto<int> input);
    }
}