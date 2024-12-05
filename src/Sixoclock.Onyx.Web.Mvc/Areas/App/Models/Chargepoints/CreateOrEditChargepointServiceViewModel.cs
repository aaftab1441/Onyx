using Abp.AutoMapper;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Chargepoints
{
    [AutoMapFrom(typeof(GetServicePriceParameterListOutput))]
    public class CreateOrEditChargepointServiceViewModel : GetChargepointServiceForEditOutput
    {
        public CreateOrEditChargepointServiceViewModel(GetChargepointServiceForEditOutput output)
        {
            output.MapTo(this);
        }
    }
   
}
