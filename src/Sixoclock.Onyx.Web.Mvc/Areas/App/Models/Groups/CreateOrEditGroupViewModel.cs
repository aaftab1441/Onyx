using Abp.AutoMapper;
using Sixoclock.Onyx.Groups.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Groups
{
    [AutoMapFrom(typeof(GetGroupForEditOutput))]
    public class CreateOrEditGroupViewModel : GetGroupForEditOutput
    {
        public CreateOrEditGroupViewModel(GetGroupForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
