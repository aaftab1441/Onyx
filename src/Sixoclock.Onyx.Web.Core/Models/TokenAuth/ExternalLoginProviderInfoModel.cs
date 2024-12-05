using Abp.AutoMapper;
using Sixoclock.Onyx.Web.Authentication.External;

namespace Sixoclock.Onyx.Web.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
