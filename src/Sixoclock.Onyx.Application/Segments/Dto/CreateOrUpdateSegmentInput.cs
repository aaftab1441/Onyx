using Abp.AutoMapper;

namespace Sixoclock.Onyx.Segments.Dto
{
    [AutoMapTo(typeof(Segment))]
    public class CreateOrUpdateSegmentInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
