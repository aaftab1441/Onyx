using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.ModelKeyValues.Dto
{
    [AutoMapFrom(typeof(ModelKeyValue))]
    public class ModelKeyValueDto : FullAuditedEntityDto
    {
        public string ModelValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }
        public int ChargepointModelId { get; set; }
        public int TenantId { get; set; }
        public int KeyValueId { get; set; }
        
        public int OCPPFeatureId { get; set; }
        public string VendorName { get; internal set; }
        public string ModelName { get; internal set; }
        public string VersionName { get; internal set; }
        public string FeatureName { get; internal set; }
        public string Key { get; internal set; }
    }
}
