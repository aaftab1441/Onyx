using Abp.AutoMapper;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Groups
{
    [AutoMapFrom(typeof(GetGroupServiceForEditOutput))]
    public class CreateOrEditGroupServiceViewModel : GetGroupServiceForEditOutput
    {
        public CreateOrEditGroupServiceViewModel(GetGroupServiceForEditOutput output)
        {
            output.MapTo(this);
        }
    }
   
}
