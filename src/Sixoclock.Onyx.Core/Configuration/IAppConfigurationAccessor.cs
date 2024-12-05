using Microsoft.Extensions.Configuration;

namespace Sixoclock.Onyx.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
