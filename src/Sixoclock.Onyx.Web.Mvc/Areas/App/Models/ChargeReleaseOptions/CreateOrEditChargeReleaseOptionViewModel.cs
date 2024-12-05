using Abp.AutoMapper;
using Sixoclock.Onyx.ChargeReleaseOptions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ChargeReleaseOptions
{
    [AutoMapFrom(typeof(GetChargeReleaseOptionForEditOutput))]
    public class CreateOrEditChargeReleaseOptionViewModel : GetChargeReleaseOptionForEditOutput
    {
        public CreateOrEditChargeReleaseOptionViewModel(GetChargeReleaseOptionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
