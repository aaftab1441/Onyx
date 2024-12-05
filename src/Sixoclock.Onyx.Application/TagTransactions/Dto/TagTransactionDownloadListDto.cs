namespace Sixoclock.Onyx.TagTransactions.Dto
{
    public class TagTransactionDownloadListDto
    {
        public string Date { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Duration { get; set; }
        public string KwhDelivered { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string CostKwh { get; set; }
        public string CostMin { get; set; }
        public string TagId { get; set; }
        public string Install { get; set; }
        public string Group { get; set; }
        public int EVSE { get; set; }
        public string Chargepoint { get; set; }
    }
}
