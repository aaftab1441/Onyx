using Abp.AutoMapper;
using System;

namespace Sixoclock.Onyx.ClearCacheEvents.Dto
{
    [AutoMapTo(typeof(ClearCacheEvent))]
    public class CreateOrUpdateClearCacheEventInput
    {
        public int Id { get; set;}
        public int ChargepointId { get; set; }
        public int ClearCacheStatusId { get; set; }
        public int? OcppMessageEventId { get; set; }
        public DateTime Date { get; set; }
        public int TenantId { get; set; }
    }
}
