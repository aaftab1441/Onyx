using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Segments.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Segments
{
    [AutoMapFrom(typeof(ListResultDto<segmentListDtos>))]
    public class SegmentsViewModel : ListResultDto<segmentListDtos>
    {
        public SegmentsViewModel(ListResultDto<segmentListDtos> output)
        {
            output.MapTo(this);
        }
    }
}
