using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ComOptions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ComOptions
{
    [AutoMapFrom(typeof(ListResultDto<ComOptionDto>))]
    public class ComOptionsViewModel : ListResultDto<ComOptionDto>
    {
        public ComOptionsViewModel(ListResultDto<ComOptionDto> output)
        {
            output.MapTo(this);
        }
    }
}
