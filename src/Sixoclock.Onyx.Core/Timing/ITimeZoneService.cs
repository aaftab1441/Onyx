using System.Threading.Tasks;
using Abp.Configuration;

namespace Sixoclock.Onyx.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
