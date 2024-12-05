using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Sixoclock.Onyx.Configuration;
using Sixoclock.Onyx.Web;

namespace Sixoclock.Onyx.API.Json.Startup
{
    [DependsOn(
        typeof(OnyxWebCoreModule))]
    public class OnyxWebJsonModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public OnyxWebJsonModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = _appConfiguration["App:ServerRootAddress"] ?? "http://localhost:22742/";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(OnyxWebJsonModule).GetAssembly());
        }
    }
}
