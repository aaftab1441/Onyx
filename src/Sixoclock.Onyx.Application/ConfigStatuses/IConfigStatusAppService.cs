using Abp.Application.Services;
using Sixoclock.Onyx.ConfigStatuses.Dto;

namespace Sixoclock.Onyx.ConfigStatuses
{
    public interface IConfigStatusAppService : IApplicationService
    {
        int GetConfigStatus(string input);
        GetConfigStatusesListOutput GetConfigStatussList();
    }
}