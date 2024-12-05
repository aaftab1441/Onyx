using Abp.AutoMapper;
using Sixoclock.Onyx.Sessions.Dto;

namespace Sixoclock.Onyx.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}