using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.AvailabilityEvents.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.AvailabilityEvents
{
    public interface IAvailabilityEventAppService : IApplicationService
    {
        Task<int> CreateOrUpdateAvailabilityEvent(CreateOrUpdateAvailabilityEventInput input);
        Task DeleteAvailabilityEvent(EntityDto<int> input);
        ListResultDto<AvailabilityEventDto> GetAvailabilityEvent(GetAvailabilityEventInput input);
        Task<GetAvailabilityEventForEditOutput> GetAvailabilityEventForEdit(EntityDto<int> input);
        GetAvailabilityEventsListOutput GetAvailabilityEventsList();
        ListResultDto<AvailabilityEventDto> GetAvailabilityEventsListByChargepoint(EntityDto<int> input);
    }
}