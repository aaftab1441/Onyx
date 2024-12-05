using Abp.AutoMapper;

namespace Sixoclock.Onyx.MeterTypes.Dto
{
    [AutoMapTo(typeof(MeterType))]
    public class CreateOrUpdateMeterTypeInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
    }
}
