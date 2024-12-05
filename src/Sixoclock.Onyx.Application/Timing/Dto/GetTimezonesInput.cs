using Abp.Configuration;

namespace Sixoclock.Onyx.Timing.Dto
{
    public class GetTimezonesInput
    {
        public SettingScopes DefaultTimezoneScope { get; set; }
    }
}
