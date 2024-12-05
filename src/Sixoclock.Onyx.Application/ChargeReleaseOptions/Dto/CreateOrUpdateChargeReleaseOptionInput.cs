using Abp.AutoMapper;

namespace Sixoclock.Onyx.ChargeReleaseOptions.Dto
{
    [AutoMapTo(typeof(ChargeReleaseOption))]
    public class CreateOrUpdateChargeReleaseOptionInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
    }
}
