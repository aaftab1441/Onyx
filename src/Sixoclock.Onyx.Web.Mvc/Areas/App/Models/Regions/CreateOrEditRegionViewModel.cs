using Abp.AutoMapper;
using Sixoclock.Onyx.Regions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Regions
{
    [AutoMapFrom(typeof(GetRegionForEditOutput))]
    public class CreateOrEditRegionViewModel: GetRegionForEditOutput
    {
        public CreateOrEditRegionViewModel(GetRegionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
