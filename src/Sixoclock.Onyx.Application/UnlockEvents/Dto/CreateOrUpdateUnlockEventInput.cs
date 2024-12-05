using Abp.AutoMapper;
using System;

namespace Sixoclock.Onyx.UnlockEvents.Dto
{
    [AutoMapTo(typeof(UnlockEvent))]
    public class CreateOrUpdateUnlockEventInput
    {
        public int Id { get; set;}
        public int EVSEId { get; set; }
        public int? UnlockStatusId { get; set; }
        public int TenantId { get; set; }
        public DateTime Date { get; set; }
        public int? OcppMessageEventId { get; set; }
    }
}
