namespace Sixoclock.Onyx.Chargepoints.Dto
{
    public class CreateResetStatusAndEventInput
    {
        public int ChargepointId { get; set; }
        public int ResetTypeId { get; set; }
        public string ResetType { get; set; }
    }
}
