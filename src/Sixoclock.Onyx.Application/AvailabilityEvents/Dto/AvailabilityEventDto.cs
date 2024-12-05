using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.AvailabilityEvents.Dto
{
    [AutoMapFrom(typeof(AvailabilityEvent))]
    public class AvailabilityEventDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int AvailabilityStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public int AvailabilityTypeId { get; set; }
        public int EVSEId { get; set; }
        public int TenantId { get; set; }
        public DateTime Date { get; set; }
        public object MessageEventResponse { get; internal set; }
        public string AvailabilityStatusValue { get; internal set; }
        public object AvailabilityTypeValue { get; internal set; }
    }
}
