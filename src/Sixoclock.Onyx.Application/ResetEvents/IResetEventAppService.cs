using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ResetEvents.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ResetEvents
{
    public interface IResetEventAppService : IApplicationService
    {
        Task<int> CreateOrUpdateResetEvent(CreateOrUpdateResetEventInput input);
        Task DeleteResetEvent(EntityDto<int> input);
        ListResultDto<ResetEventDto> GetResetEvent(GetResetEventInput input);
        Task<GetResetEventForEditOutput> GetResetEventForEdit(EntityDto<int> input);
        GetResetEventsListOutput GetResetEventsList();
        ListResultDto<ResetEventDto> GetResetEventsListByChargepoint(EntityDto<int> input);
    }
}