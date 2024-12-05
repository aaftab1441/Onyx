using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Chargepoints.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Chargepoints
{
    [AutoMapFrom(typeof(ListResultDto<ChargepointDto>))]
    public class ChargepointsViewModel : ListResultDto<ChargepointDto>
    {
        public ChargepointsViewModel(ListResultDto<ChargepointDto> output)
        {
            output.MapTo(this);
        }
    }
}
