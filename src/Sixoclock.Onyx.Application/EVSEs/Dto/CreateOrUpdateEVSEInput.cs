using Abp.AutoMapper;

namespace Sixoclock.Onyx.EVSEs.Dto
{
    [AutoMapTo(typeof(EVSE))]
    public class CreateOrUpdateEVSEInput
    {
        public int Id { get; set; }
        public int ChargepointId { get; set; }
        public int? MeterTypeId { get; set; }
        public int? AvailabilityTypeId { get; set; }
        public int? EVSEStatusStatusId { get; set; }
        public string Comment { get; set; }
        public int EVSE_id { get; set; }
        public int TenantId { get; set; }
        public bool IsDeleted { get; internal set; }
    }
}
