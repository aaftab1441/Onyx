using Abp.AutoMapper;
using Sixoclock.Onyx.Tags.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Tags
{
    [AutoMapFrom(typeof(GetTagForEditOutput))]
    public class CreateOrEditTagViewModel : GetTagForEditOutput
    {
        public CreateOrEditTagViewModel(GetTagForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
