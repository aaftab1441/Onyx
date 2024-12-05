using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Capacities.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Capacities
{
    [AutoMapFrom(typeof(ListResultDto<CapacityDto>))]
    public class CapacitiesViewModel : ListResultDto<CapacityDto>
    {
        public CapacitiesViewModel(ListResultDto<CapacityDto> output)
        {
            output.MapTo(this);
        }
    }
}
