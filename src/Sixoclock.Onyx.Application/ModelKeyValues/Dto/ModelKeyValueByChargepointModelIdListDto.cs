namespace Sixoclock.Onyx.ModelKeyValues.Dto
{
    public class ModelKeyValueByChargepointModelIdListDto
    {
        public int Id { get; set; }
        public string ModelValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }
        public int KeyValueId { get; set; }
        public string Key { get; set; }
        public string FeatureName { get; set; }

        public int ChargepointModelId { get; set; }
        public int TenantId { get; set; }
    }
}
