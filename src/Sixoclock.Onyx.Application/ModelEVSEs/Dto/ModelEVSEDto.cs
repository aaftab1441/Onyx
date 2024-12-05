using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.ModelEVSEs.Dto
{
    [AutoMapFrom(typeof(ModelEVSE))]
    public class ModelEVSEDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int? MeterTypeId { get; set; }
        public int ChargepointModelId { get; set; }
        public int EVSEId { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
        public string Vendor { get; internal set; }
        public string ModelName { get; internal set; }
        public string MeterType { get; internal set; }
        public int? ConnectorsCount { get; internal set; }
        public int? VendorId { get; internal set; }
    }
}
