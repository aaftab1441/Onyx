using Abp.AutoMapper;
using Sixoclock.Onyx.MultiTenancy.Dto;

namespace Sixoclock.Onyx.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}