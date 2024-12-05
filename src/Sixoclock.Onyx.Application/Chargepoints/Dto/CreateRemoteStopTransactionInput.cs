namespace Sixoclock.Onyx.Chargepoints.Dto
{
    public class CreateRemoteStopTransactionInput
    {
        public int ChargepointId { get; set; }
        public int EVSEId { get; set; }
        public int TransactionId { get; set; }
    }
}
