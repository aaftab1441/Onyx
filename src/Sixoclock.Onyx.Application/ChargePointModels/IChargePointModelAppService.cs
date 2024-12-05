using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ChargePointModels.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ChargePointModels
{
    public interface IChargePointModelAppService : IApplicationService
    {
        Task<int> CreateOrUpdateChargepointModel(CreateOrUpdateChargepointModelInput input);
        Task CreateOrUpdateChargepointModelOptions(CreateOrUpdateChargepointModelOptionsInput input);
        Task DeleteChargepointModel(EntityDto<int> input);
        Task<PagedResultDto<ChargepointModelDto>> GetChargepointModel(GetChargepointModelInput input);
        Task<GetChargepointModelForEditOutput> GetChargepointModelForEdit(EntityDto<int> input);
        GetChargepointModelsListOutput GetChargepointModelsList();
        GetChargepointModelsListOutput GetChargepointModelsByVendorList(EntityDto<int> input);
        GetChargepointModelsListOutput GetChargepointModelsByMountList(EntityDto<int> input);
        GetChargepointModelForChargepointOutput GetChargepointModelForChargepoint(EntityDto<int> input);
    }
}