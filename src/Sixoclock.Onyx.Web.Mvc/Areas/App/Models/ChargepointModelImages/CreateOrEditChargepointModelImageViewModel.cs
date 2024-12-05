using Abp.AutoMapper;
using Sixoclock.Onyx.ChargepointModelImages.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ChargepointModelImages
{
    [AutoMapFrom(typeof(GetChargepointModelImageForEditOutput))]
    public class CreateOrEditChargepointModelImageViewModel : GetChargepointModelImageForEditOutput
    {
        public CreateOrEditChargepointModelImageViewModel(GetChargepointModelImageForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
