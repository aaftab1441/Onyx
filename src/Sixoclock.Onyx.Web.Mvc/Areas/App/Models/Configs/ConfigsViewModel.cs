using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Configs.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Configs
{
    [AutoMapFrom(typeof(ListResultDto<ConfigDto>))]
    public class ConfigsViewModel : ListResultDto<ConfigDto>
    {
        public ConfigsViewModel(ListResultDto<ConfigDto> output)
        {
            output.MapTo(this);
        }
    }
}
