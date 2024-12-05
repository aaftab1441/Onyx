using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.UnlockEvents.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.UnlockEvents
{
    public interface IUnlockEventAppService : IApplicationService
    {
        Task<int> CreateOrUpdateUnlockEvent(CreateOrUpdateUnlockEventInput input);
        Task DeleteUnlockEvent(EntityDto<int> input);
        ListResultDto<UnlockEventDto> GetUnlockEvent(GetUnlockEventInput input);
        Task<GetUnlockEventForEditOutput> GetUnlockEventForEdit(EntityDto<int> input);
        GetUnlockEventsListOutput GetUnlockEventsList();
        ListResultDto<UnlockEventDto> GetUnlockEventsListByChargepoint(EntityDto<int> input);
    }
}