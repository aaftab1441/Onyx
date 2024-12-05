using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;

namespace Sixoclock.Onyx.ChargepointKeyValues.Dto
{
    [AutoMapFrom(typeof(ChargepointKeyValue))]
    public class ChargepointKeyValueDto : FullAuditedEntityDto,IMustHaveTenant
    {
        public string ChargepointValue { get; set; }
        public string WildValue { get; set; }
        public string RW { get; set; }
        public string Comment { get; set; }

        public int ChargepointId { get; set; }
        public int KeyValueId { get; set; }
        public int TenantId { get; set; }
        
        public string FeatureName { get; set; }
        public string Key { get; set; }
        public string VendorName { get; internal set; }
        public string ModelName { get; internal set; }
        public string Identity { get; internal set; }
        public string VersionName { get; internal set; }
        public int ModelKeyValueId { get; internal set; }
    }
}
