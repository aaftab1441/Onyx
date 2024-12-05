using Abp.Application.Services;
using Sixoclock.Onyx.ClearCacheStatuses.Dto;

namespace Sixoclock.Onyx.ClearCacheStatuses
{
    public interface IClearCacheStatusAppService : IApplicationService
    {
        int GetClearCacheStatus(string input);
        GetClearCacheStatusesListOutput GetClearCacheStatussList();
    }
}