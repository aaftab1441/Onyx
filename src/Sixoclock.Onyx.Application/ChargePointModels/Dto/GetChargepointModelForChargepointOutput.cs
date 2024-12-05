namespace Sixoclock.Onyx.ChargePointModels.Dto
{
    public class GetChargepointModelForChargepointOutput
    {
        public string Vendor { get; set; }
        public string Capacity { get; set; }
        public int NoOfConnectors { get; internal set; }
        public int OCPPTransportId { get; internal set; }
        public int OCPPVersionId { get; internal set; }
    }
}
