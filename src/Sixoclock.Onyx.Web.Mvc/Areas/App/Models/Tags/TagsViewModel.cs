using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Tags.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Tags
{
    [AutoMapFrom(typeof(ListResultDto<TagDto>))]
    public class TagsViewModel : ListResultDto<TagDto>
    {
        public TagsViewModel(ListResultDto<TagDto> output)
        {
            output.MapTo(this);
        }
    }
}
