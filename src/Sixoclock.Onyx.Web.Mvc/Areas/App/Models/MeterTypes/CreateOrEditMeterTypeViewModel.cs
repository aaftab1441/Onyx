using Abp.AutoMapper;
using Sixoclock.Onyx.MeterTypes.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.MeterTypes
{
    [AutoMapFrom(typeof(GetMeterTypeForEditOutput))]
    public class CreateOrEditMeterTypeViewModel : GetMeterTypeForEditOutput
    {
        public CreateOrEditMeterTypeViewModel(GetMeterTypeForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
