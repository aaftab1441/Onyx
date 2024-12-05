using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.EVSEs.Dto
{
    [AutoMapFrom(typeof(EVSE))]
    public class EVSEDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int ChargepointId { get; set; }
        public int? MeterTypeId { get; set; }
        public int? AvailabilityTypeId { get; set; }
        public int? EVSEStatusId { get; set; }
        public string Comment { get; set; }
        public int EVSE_id { get; set; }
        public int TenantId { get; set; }
        
        public int VendorId { get; set; }
        public string AvailabilityType { get; set; }
        public string Vendor { get; internal set; }
        public int ConnectorsCount { get; set; }
        public string MeterType { get; set; }
        public string EVSEStatus { get; set; }
        public string ModelName { get; set; }
        public string EVSEError { get; set; }
    }
}
