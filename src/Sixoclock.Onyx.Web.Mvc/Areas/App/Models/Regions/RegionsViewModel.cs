using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Regions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Regions
{
    [AutoMapFrom(typeof(ListResultDto<RegionDto>))]
    public class RegionsViewModel: ListResultDto<RegionDto>
    {
        public RegionsViewModel(ListResultDto<RegionDto> output)
        {
            output.MapTo(this);
        }
    }
}
