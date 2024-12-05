using System.Threading.Tasks;
using Abp.Application.Services;
using Sixoclock.Onyx.Configuration.Host.Dto;

namespace Sixoclock.Onyx.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
