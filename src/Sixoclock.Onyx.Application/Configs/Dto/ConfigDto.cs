using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Configs.Dto
{
    [AutoMapFrom(typeof(KeyValue))]
    public class ConfigDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string Key { get; set; }
        public string DefaultValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }
        public int OCPPFeatureId { get; set; }

        public string FeatureName { get; set; }
        public string VersionName { get; set; }
        public int OCPPVersionId { get; internal set; }
    }
}
