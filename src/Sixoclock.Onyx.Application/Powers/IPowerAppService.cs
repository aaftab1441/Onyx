using Abp.Application.Services;
using Sixoclock.Onyx.Powers.Dto;

namespace Sixoclock.Onyx.Powers
{
    public interface IPowerAppService : IApplicationService
    {
        GetPowersListOutput GetPowersList();
    }
}