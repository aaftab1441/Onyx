using Abp.AutoMapper;
using Sixoclock.Onyx.Tags.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Tags
{
    [AutoMapFrom(typeof(TagDto))]
    public class TagViewDetails : TagDto
    {
        public TagViewDetails(TagDto output)
        {
            output.MapTo(this);
        }
    }
}
