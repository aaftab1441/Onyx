using System.Collections.Generic;

namespace Sixoclock.Onyx.Segments.Dto
{
    public class GetSegmentsListOutput
    {
        public IEnumerable<SegmentListDto> Segments { get; set; }
    }
}
