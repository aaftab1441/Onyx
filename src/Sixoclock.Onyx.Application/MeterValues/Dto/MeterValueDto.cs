using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.MeterValues.Dto
{
    [AutoMapFrom(typeof(MeterValue))]
    public class MeterValueDto : FullAuditedEntityDto, IHasCreationTime
    {
        public float MeterValue { get; set; }
        public DateTime MeterTime { get; set; }

        public int MeasurandId { get; set; }
        public int ContextId { get; set; }
        public int LocationId { get; set; }
        public int MeterValueTypeId { get; set; }
        public int PhaseId { get; set; }
        public int FormatId { get; set; }
        public int UnitId { get; set; }
        public int TenantId { get; set; }
        public string UnitName { get; internal set; }
        public string MeterValueType { get; internal set; }
        public string MeasurandType { get; internal set; }
        public string ContextName { get; internal set; }
        public string LocationName { get; internal set; }
        public string PhaseName { get; internal set; }
        public string FormatType { get; internal set; }
    }
}
