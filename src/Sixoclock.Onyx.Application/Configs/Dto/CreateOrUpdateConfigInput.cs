using Abp.AutoMapper;

namespace Sixoclock.Onyx.Configs.Dto
{
    [AutoMapTo(typeof(KeyValue))]
    public class CreateOrUpdateConfigInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Key { get; set; }
        public string DefaultValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }
        public int OCPPFeatureId { get; set; }
    }
}
