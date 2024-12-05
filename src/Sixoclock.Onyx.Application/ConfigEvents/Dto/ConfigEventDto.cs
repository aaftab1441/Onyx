using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.ConfigEvents.Dto
{
    [AutoMapFrom(typeof(ConfigEvent))]
    public class ConfigEventDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int ChargepointId { get; set; }
        public int ConfigTypeId { get; set; }
        public int TenantId { get; set; }
        public int ConfigStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public DateTime Date { get; set; }
        public object MessageEventResponse { get; internal set; }
        public string ConfigStatusType { get; internal set; }
        public string ConfigStatusValue { get; internal set; }
    }
}
