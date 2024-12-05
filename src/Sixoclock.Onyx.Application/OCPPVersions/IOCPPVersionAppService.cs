using Abp.Application.Services;
using Sixoclock.Onyx.OCPPVersions.Dto;

namespace Sixoclock.Onyx.OCPPVersions
{
    public interface IOCPPVersionAppService : IApplicationService
    {
        GetOCPPVersionsListOutput GetOCPPVersionsList();
    }
}