using Abp.Application.Services;
using Sixoclock.Onyx.AvailabilityTypes.Dto;

namespace Sixoclock.Onyx.AvailabilityTypes
{
    public interface IAvailabilityTypeAppService : IApplicationService
    {
        GetAvailabilityTypesListOutput GetAvailabilityTypesList();
    }
}