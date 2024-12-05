using Abp.Application.Services;
using Sixoclock.Onyx.UnlockStatuses.Dto;

namespace Sixoclock.Onyx.UnlockStatuses
{
    public interface IUnlockStatusAppService : IApplicationService
    {
        int GetUnlockStatus(string input);
        GetUnlockStatusesListOutput GetUnlockStatussList();
    }
}