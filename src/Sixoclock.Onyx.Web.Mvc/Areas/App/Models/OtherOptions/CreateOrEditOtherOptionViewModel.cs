using Abp.AutoMapper;
using Sixoclock.Onyx.OtherOptions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.OtherOptions
{
    [AutoMapFrom(typeof(GetOtherOptionForEditOutput))]
    public class CreateOrEditOtherOptionViewModel : GetOtherOptionForEditOutput
    {
        public CreateOrEditOtherOptionViewModel(GetOtherOptionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
