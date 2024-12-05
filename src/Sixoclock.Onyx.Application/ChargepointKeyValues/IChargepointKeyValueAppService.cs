using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ChargepointKeyValues.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ChargepointKeyValues
{
    public interface IChargepointKeyValueAppService : IApplicationService
    {
        Task CreateOrUpdateChargepointKeyValue(CreateOrUpdateChargepointKeyValueInput input);
        Task<PagedResultDto<ChargepointKeyValueDto>> GetChargepointKeyValue(GetChargepointKeyValueInput input);
        Task<PagedResultDto<ChargepointKeyValueDto>> GetChargepointKeyValuesListByChargepointAndTenant(GetChargepointKeyValuesListByChargepointInput input);
        Task<GetChargepointKeyValueForEditOutput> GetChargepointKeyValueForEdit(EntityDto<int> input);
        Task DeleteChargepointKeyValue(EntityDto<int> input);
        Task DeleteChargepointKeyValuesByChargepoint(EntityDto<int> input);
        Task CopyModelKeyValuesToChargepointKeyValues(int chargepointModelId, int chargepointId);
    }
}