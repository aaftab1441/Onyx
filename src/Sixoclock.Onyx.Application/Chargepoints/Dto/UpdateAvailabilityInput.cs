namespace Sixoclock.Onyx.Chargepoints.Dto
{
    public class UpdateAvailabilityInput
    {
        public int ChargepointId { get; set; }
        public string Availability { get; set; }
        public int AvailabilityTypeId { get; set; }
        public int EVSEId { get; set; }
    }
}
