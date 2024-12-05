using Abp.AutoMapper;

namespace Sixoclock.Onyx.ElectricalOptions.Dto
{
    [AutoMapTo(typeof(ElectricalOption))]
    public class CreateOrUpdateElectricalOptionInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
    }
}
