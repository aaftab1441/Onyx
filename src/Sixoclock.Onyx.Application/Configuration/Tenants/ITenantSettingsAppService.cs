using System.Threading.Tasks;
using Abp.Application.Services;
using Sixoclock.Onyx.Configuration.Tenants.Dto;

namespace Sixoclock.Onyx.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
