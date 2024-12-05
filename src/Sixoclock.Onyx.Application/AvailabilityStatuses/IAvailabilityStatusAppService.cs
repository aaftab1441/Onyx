using Abp.Application.Services;
using Sixoclock.Onyx.AvailabilityStatuses.Dto;

namespace Sixoclock.Onyx.AvailabilityStatuses
{
    public interface IAvailabilityStatusAppService : IApplicationService
    {
        int GetAvailabilityStatus(string input);
        GetAvailabilityStatusesListOutput GetAvailabilityStatussList();
    }
}