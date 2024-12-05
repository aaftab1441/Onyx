using Abp.AutoMapper;

namespace Sixoclock.Onyx.ChargepointKeyValues.Dto
{
    [AutoMapTo(typeof(ChargepointKeyValue))]
    public class UpdateChargepointKeyValueInput
    {
        public int Id { get; set; }
        public string ChargepointValue { get; set; }
        public string WildValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }

        public int ChargepointId { get; set; }
        public int KeyValueId { get; set; }
        public int TenantId { get; set; }
        public string FeatureName { get; set; }
        public string Key { get; set; }
    }
}
