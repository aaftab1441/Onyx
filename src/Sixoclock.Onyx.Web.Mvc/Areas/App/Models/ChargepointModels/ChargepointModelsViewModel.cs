using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ChargePointModels.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ChargepointModels
{
    [AutoMapFrom(typeof(ListResultDto<ChargepointModelDto>))]
    public class ChargepointModelsViewModel : ListResultDto<ChargepointModelDto>
    {
        public ChargepointModelsViewModel(ListResultDto<ChargepointModelDto> output)
        {
            output.MapTo(this);
        }
    }
}
