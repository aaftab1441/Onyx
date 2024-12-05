using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ParentTags.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ParentTags
{
    [AutoMapFrom(typeof(ListResultDto<ParentTagDto>))]
    public class ParentTagsViewModel : ListResultDto<ParentTagDto>
    {
        public ParentTagsViewModel(ListResultDto<ParentTagDto> output)
        {
            output.MapTo(this);
        }
    }
}
