using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ConfigEvents.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ConfigEvents
{
    public interface IConfigEventAppService : IApplicationService
    {
        Task<int> CreateOrUpdateConfigEvent(CreateOrUpdateConfigEventInput input);
        Task DeleteConfigEvent(EntityDto<int> input);
        ListResultDto<ConfigEventDto> GetConfigEvent(GetConfigEventInput input);
        Task<GetConfigEventForEditOutput> GetConfigEventForEdit(EntityDto<int> input);
        GetConfigEventsListOutput GetConfigEventsList();
        ListResultDto<ConfigEventDto> GetConfigEventsListByChargepoint(EntityDto<int> input);
    }
}