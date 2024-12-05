using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.ResetEvents.Dto
{
    [AutoMapFrom(typeof(ResetEvent))]
    public class ResetEventDto : FullAuditedEntityDto, IHasCreationTime
    {
        public DateTime Date { get; set; }
        public int ChargepointId { get; set; }
        public int ResetTypeId { get; set; }
        public string MessageEventResponse { get; internal set; }
        public string ResetType { get; internal set; }
        public string ResetStatusValue { get; internal set; }
        public int TenantId { get; set; }
        public object Type { get; internal set; }
    }
}
