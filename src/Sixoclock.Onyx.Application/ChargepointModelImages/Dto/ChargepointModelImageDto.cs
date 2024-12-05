using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.ChargepointModelImages.Dto
{
    [AutoMapFrom(typeof(ChargepointModelImage))]
    public class ChargepointModelImageDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public string Comment { get; set; }
        public string OriginalFileName { get; set; }
        public string Ext { get; set; }
        public bool IsActive { get; set; }
        public int ChargepointModelId { get; set; }
        public string ModelName { get; set; }

    }
}
