using Abp.AutoMapper;
using System;

namespace Sixoclock.Onyx.ConfigEvents.Dto
{
    [AutoMapTo(typeof(ConfigEvent))]
    public class CreateOrUpdateConfigEventInput
    {
        public int Id { get; set;}
        public int ChargepointId { get; set; }
        public int ConfigTypeId { get; set; }
        public int TenantId { get; set; }
        public int ConfigStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public DateTime Date { get; set; }
    }
}
