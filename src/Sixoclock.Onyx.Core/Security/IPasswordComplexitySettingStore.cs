using System.Threading.Tasks;

namespace Sixoclock.Onyx.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
