using System.Collections.Generic;

namespace Sixoclock.Onyx.Web.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}