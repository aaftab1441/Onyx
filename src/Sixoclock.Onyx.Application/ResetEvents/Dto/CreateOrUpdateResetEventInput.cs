using Abp.AutoMapper;
using System;

namespace Sixoclock.Onyx.ResetEvents.Dto
{
    [AutoMapTo(typeof(ResetEvent))]
    public class CreateOrUpdateResetEventInput
    {
        public int Id { get; set;}
        public DateTime Date { get; set; }

        public int ChargepointId { get; set; }
        public int ResetTypeId { get; set; }
        public int TenantId { get; set; }
        public int ResetStatusId { get; set; }
    }
}
