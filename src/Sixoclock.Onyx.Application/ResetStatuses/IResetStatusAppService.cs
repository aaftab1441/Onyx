using Abp.Application.Services;
using Sixoclock.Onyx.ResetStatuses.Dto;

namespace Sixoclock.Onyx.ResetStatuses
{
    public interface IResetStatusAppService : IApplicationService
    {
        int GetResetStatus(string input);
        GetResetStatusesListOutput GetResetStatussList();
    }
}