using Abp.AutoMapper;
using Sixoclock.Onyx.MultiTenancy;
using Sixoclock.Onyx.MultiTenancy.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Common;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }

        public TenantFeaturesEditViewModel(Tenant tenant, GetTenantFeaturesEditOutput output)
        {
            Tenant = tenant;
            output.MapTo(this);
        }
    }
}