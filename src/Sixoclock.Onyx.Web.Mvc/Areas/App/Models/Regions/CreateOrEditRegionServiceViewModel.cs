using Abp.AutoMapper;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Regions
{
    [AutoMapFrom(typeof(GetRegionServiceForEditOutput))]
    public class CreateOrEditRegionServiceViewModel : GetRegionServiceForEditOutput
    {
        public CreateOrEditRegionServiceViewModel(GetRegionServiceForEditOutput output)
        {
            output.MapTo(this);
        }
    }
   
}
