using Abp.AutoMapper;
using Sixoclock.Onyx.ParentTags.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ParentTags
{
    [AutoMapFrom(typeof(GetParentTagForEditOutput))]
    public class CreateOrEditParentTagViewModel : GetParentTagForEditOutput
    {
        public CreateOrEditParentTagViewModel(GetParentTagForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
