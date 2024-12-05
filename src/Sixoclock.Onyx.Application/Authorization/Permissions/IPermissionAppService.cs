using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Authorization.Permissions.Dto;

namespace Sixoclock.Onyx.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
