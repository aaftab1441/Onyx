using Abp.AutoMapper;
using Sixoclock.Onyx.MultiTenancy.Dto;

namespace Sixoclock.Onyx.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
        public EditionsSelectViewModel(EditionsSelectOutput output)
        {
            output.MapTo(this);
        }
    }
}
