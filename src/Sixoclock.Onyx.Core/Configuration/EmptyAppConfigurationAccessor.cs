using Abp.Dependency;
using Microsoft.Extensions.Configuration;

namespace Sixoclock.Onyx.Configuration
{
    /* This service is replaced in Web layer and Test project separately */
    public class EmptyAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }
        
        public EmptyAppConfigurationAccessor()
        {
            Configuration = new ConfigurationBuilder().Build();
        }
    }
}