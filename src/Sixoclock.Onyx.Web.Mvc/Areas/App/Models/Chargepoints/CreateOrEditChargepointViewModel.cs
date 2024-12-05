using Abp.AutoMapper;
using Sixoclock.Onyx.Chargepoints.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Chargepoints
{
    [AutoMapFrom(typeof(GetChargepointForEditOutput))]
    public class CreateOrEditChargepointViewModel : GetChargepointForEditOutput
    {
        public CreateOrEditChargepointViewModel(GetChargepointForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
