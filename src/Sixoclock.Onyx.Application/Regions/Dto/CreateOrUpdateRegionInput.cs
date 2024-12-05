using Abp.AutoMapper;

namespace Sixoclock.Onyx.Regions.Dto
{
    [AutoMapTo(typeof(Region))]
    public class CreateOrUpdateRegionInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int MarketId { get; set; }
        public string RegionName { get; set; }
    }
}
