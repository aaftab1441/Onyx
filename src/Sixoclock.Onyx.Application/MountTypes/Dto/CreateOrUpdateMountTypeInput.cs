using Abp.AutoMapper;

namespace Sixoclock.Onyx.MountTypes.Dto
{
    [AutoMapTo(typeof(MountType))]
    public class CreateOrUpdateMountTypeInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
