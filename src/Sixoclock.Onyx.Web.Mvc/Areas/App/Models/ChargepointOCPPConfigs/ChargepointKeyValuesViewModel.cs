using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ChargepointKeyValues.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ChargepointOCPPConfigs
{
    [AutoMapFrom(typeof(ListResultDto<ChargepointKeyValueDto>))]
    public class ChargepointKeyValuesViewModel : ListResultDto<ChargepointKeyValueDto>
    {
        public ChargepointKeyValuesViewModel(ListResultDto<ChargepointKeyValueDto> output)
        {
            output.MapTo(this);
        }
    }
}
