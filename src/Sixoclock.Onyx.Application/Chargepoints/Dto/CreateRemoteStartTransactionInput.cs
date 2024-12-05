namespace Sixoclock.Onyx.Chargepoints.Dto
{
    public class CreateRemoteStartTransactionInput
    {
        public int ChargepointId { get; set; }
        public string TagIdToken { get; set; }
        public int EVSEId { get; set; }
        public int TagId { get; set; }

        public int RemoteStartStopStatusId { get; set; }
        public int RemoteStartStopEventTypeId { get; set; }
    }
}
