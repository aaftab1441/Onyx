using Abp.AutoMapper;
using Sixoclock.Onyx.ChargePointModels.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ChargepointModels
{
    [AutoMapFrom(typeof(GetChargepointModelForEditOutput))]
    public class CreateOrEditChargepointModelViewModel : GetChargepointModelForEditOutput
    {
        public CreateOrEditChargepointModelViewModel(GetChargepointModelForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
