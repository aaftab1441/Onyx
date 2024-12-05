using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Installs.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Installs
{
    public interface IInstallAppService : IApplicationService
    {
        Task CreateOrUpdateInstall(CreateOrUpdateInstallInput input);
        Task DeleteInstall(EntityDto<int> input);
        Task<PagedResultDto<InstallDto>> GetInstall(GetInstallInput input);
        Task<GetInstallForEditOutput> GetInstallForEdit(EntityDto<int> input);
        Task<GetInstallsListOutput> GetInstallsList();
        Task<GetInstallsListOutput> GetInstallsListByRegion(EntityDto<int> input);
    }
}