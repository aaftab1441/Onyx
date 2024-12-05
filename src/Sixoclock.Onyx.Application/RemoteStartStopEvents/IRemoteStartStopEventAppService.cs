using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.RemoteStartStopEvents.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.RemoteStartStopEvents
{
    public interface IRemoteStartStopEventAppService : IApplicationService
    {
        Task<int> CreateOrUpdateRemoteStartStopEvent(CreateOrUpdateRemoteStartStopEventInput input);
        Task DeleteRemoteStartStopEvent(EntityDto<int> input);
        ListResultDto<RemoteStartStopEventDto> GetRemoteStartStopEvent(GetRemoteStartStopEventInput input);
        Task<GetRemoteStartStopEventForEditOutput> GetRemoteStartStopEventForEdit(EntityDto<int> input);
        GetRemoteStartStopEventsListOutput GetRemoteStartStopEventsList();
        ListResultDto<RemoteStartStopEventDto> GetRemoteStartStopEventsListByChargepoint(EntityDto<int> input);
    }
}