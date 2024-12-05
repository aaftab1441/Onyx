using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Chargepoints.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Chargepoints
{
    public interface IChargepointAppService : IApplicationService
    {
        Task CreateOrUpdateChargepoint(CreateOrUpdateChargepointInput input);
        Task DeleteChargepoint(EntityDto<int> input);
        Task<PagedResultDto<ChargepointDto>> GetChargepoint(GetChargepointInput input);
        GetChargepointForEditOutput GetChargepointForEdit(EntityDto<int> input);
        Task<GetChargepointsListOutput> GetChargepointsList();
        Task<GetChargepointsListOutput> GetChargepointsListByGroup(EntityDto<int> input);
        Task<GetChargepointForEditOutput> GetChargepointByIdForManageChargepoint(EntityDto<int> input);
    }
}