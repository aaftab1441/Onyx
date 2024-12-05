using Abp.AutoMapper;
using System;

namespace Sixoclock.Onyx.AvailabilityEvents.Dto
{
    [AutoMapTo(typeof(AvailabilityEvent))]
    public class CreateOrUpdateAvailabilityEventInput
    {
        public int Id { get; set;}
        public int AvailabilityStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public int AvailabilityTypeId { get; set; }
        public int EVSEId { get; set; }
        public int TenantId { get; set; }
        public DateTime Date { get; set; }
    }
}
