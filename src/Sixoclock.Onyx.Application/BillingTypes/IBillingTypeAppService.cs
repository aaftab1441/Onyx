using Abp.Application.Services;
using Sixoclock.Onyx.BillingTypes.Dto;

namespace Sixoclock.Onyx.BillingTypes
{
    public interface IBillingTypeAppService : IApplicationService
    {
        GetBillingTypesListOutput GetBillingTypesList();
    }
}