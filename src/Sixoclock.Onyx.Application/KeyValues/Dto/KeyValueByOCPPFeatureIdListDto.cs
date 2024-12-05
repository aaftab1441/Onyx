namespace Sixoclock.Onyx.KeyValues.Dto
{
    public class KeyValueByOCPPFeatureIdListDto
    {
        public int Id { get; set; }
        public string DefaultValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }
        public string Key { get; set; }
        public string FeatureName { get; set; }
        public int TenantId { get; set; }
    }
}
