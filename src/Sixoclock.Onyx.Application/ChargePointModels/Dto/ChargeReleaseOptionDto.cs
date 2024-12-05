using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace Sixoclock.Onyx.ChargePointModels.Dto
{
    [AutoMapFrom(typeof(ChargeReleaseOption))]
    public class ChargeReleaseOptionDto : FullAuditedEntityDto
    {
        public string Option { get; set; }
        public string Comment { get; set; }
        public bool Assigned { get; set; }
        public int TenantId { get; set; }
        public List<ReleaseOptionModel> ReleaseOptionModels { get; set; }
    }
}
