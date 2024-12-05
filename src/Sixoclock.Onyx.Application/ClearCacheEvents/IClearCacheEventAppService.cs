using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ClearCacheEvents.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ClearCacheEvents
{
    public interface IClearCacheEventAppService : IApplicationService
    {
        Task<int> CreateOrUpdateClearCacheEvent(CreateOrUpdateClearCacheEventInput input);
        Task DeleteClearCacheEvent(EntityDto<int> input);
        ListResultDto<ClearCacheEventDto> GetClearCacheEvent(GetClearCacheEventInput input);
        Task<GetClearCacheEventForEditOutput> GetClearCacheEventForEdit(EntityDto<int> input);
        GetClearCacheEventsListOutput GetClearCacheEventsList();
        ListResultDto<ClearCacheEventDto> GetClearCacheEventsListByChargepoint(EntityDto<int> input);
    }
}