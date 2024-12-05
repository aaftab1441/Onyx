using Abp.AutoMapper;
using Sixoclock.Onyx.Segments.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Segments
{
    [AutoMapFrom(typeof(GetSegmentForEditOutput))]
    public class CreateOrEditSegmentViewModel : GetSegmentForEditOutput
    {
        public CreateOrEditSegmentViewModel(GetSegmentForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
