using Abp.AutoMapper;

namespace Sixoclock.Onyx.ParentTags.Dto
{
    [AutoMapTo(typeof(ParentTag))]
    public class CreateOrUpdateParentTagInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
    }
}
