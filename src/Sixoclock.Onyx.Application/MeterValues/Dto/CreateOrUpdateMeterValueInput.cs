using Abp.AutoMapper;
using System;

namespace Sixoclock.Onyx.MeterValues.Dto
{
    [AutoMapTo(typeof(MeterValue))]
    public class CreateOrUpdateMeterValueInput
    {
        public int Id { get; set; }
        public int MeterValue { get; set; }
        public DateTime MeterTime { get; set; }

        public int MeasurandId { get; set; }
        public int ContextId { get; set; }
        public int LocationId { get; set; }
        public int MeterValueTypeId { get; set; }
        public int PhaseId { get; set; }
        public int FormatId { get; set; }
        public int UnitId { get; set; }
        public int TenantId { get; set; }
    }
}
