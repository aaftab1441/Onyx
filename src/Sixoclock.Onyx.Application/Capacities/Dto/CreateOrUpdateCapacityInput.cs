using Abp.AutoMapper;

namespace Sixoclock.Onyx.Capacities.Dto
{
    [AutoMapTo(typeof(Capacity))]
    public class CreateOrUpdateCapacityInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int PowerId { get; set; }
        public int UnitId { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
    }
}
