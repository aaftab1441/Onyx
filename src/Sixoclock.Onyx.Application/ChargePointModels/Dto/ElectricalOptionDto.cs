using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace Sixoclock.Onyx.ChargePointModels.Dto
{
    [AutoMapFrom(typeof(ElectricalOptionDto))]
    public class ElectricalOptionDto : FullAuditedEntityDto
    {
        public int TenantId { get; set; }
        public string Option { get; set; }
        public bool Assigned { get; set; }
        public List<ElectricalOptionModel> Electrics { get; set; }
    }
}
