using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.MeterTypes.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.MeterTypes
{
    [AutoMapFrom(typeof(ListResultDto<MeterTypeDto>))]
    public class MeterTypesViewModel : ListResultDto<MeterTypeDto>
    {
        public MeterTypesViewModel(ListResultDto<MeterTypeDto> output)
        {
            output.MapTo(this);
        }
    }
}
