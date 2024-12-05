using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.MultiTenancy;

namespace Sixoclock.Onyx.Authorization.Accounts.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class CurrentTenantInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}