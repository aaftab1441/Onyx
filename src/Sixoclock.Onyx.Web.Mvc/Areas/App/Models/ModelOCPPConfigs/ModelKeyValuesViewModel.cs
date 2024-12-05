using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ModelKeyValues.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ModelOCPPConfigs
{
    [AutoMapFrom(typeof(ListResultDto<ModelKeyValueDto>))]
    public class ModelKeyValuesViewModel : ListResultDto<ModelKeyValueDto>
    {
        public ModelKeyValuesViewModel(ListResultDto<ModelKeyValueDto> output)
        {
            output.MapTo(this);
        }
    }
}
