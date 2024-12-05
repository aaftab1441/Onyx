using Abp.Dependency;
using Sixoclock.Onyx.Configuration;
using Sixoclock.Onyx.Url;
using Sixoclock.Onyx.Web.Url;

namespace Sixoclock.Onyx.Web.Public.Url
{
    public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
    {
        public WebUrlService(
            IAppConfigurationAccessor appConfigurationAccessor) :
            base(appConfigurationAccessor)
        {
        }

        public override string WebSiteRootAddressFormatKey => "App:WebSiteRootAddress";

        public override string ServerRootAddressFormatKey => "App:AdminWebSiteRootAddress";
    }
}