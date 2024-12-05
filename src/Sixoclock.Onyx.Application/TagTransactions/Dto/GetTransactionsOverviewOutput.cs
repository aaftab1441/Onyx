namespace Sixoclock.Onyx.TagTransactions.Dto
{
    public class GetTransactionsOverviewOutput
    {
        public float CurrentWeekEnergy { get; set; }
        public int CurrentWeekTransactions { get; set; }
        public int CurrentWeekChargetime { get; set; }
        public int CurrentWeekCo2Saved { get; set; }

        public float CurrentWeekEnergyChange { get; set; }
        public int CurrentWeekTransactionsChange { get; set; }
        public int CurrentWeekChargetimeChange { get; set; }
        public int CurrentWeekCo2SavedChange { get; set; }

        public float? TotalDeliveredEnergy { get; set; }
        public int? TotalTransactions { get; set; }
        public int TotalChargeTime { get; set; }
        public int TotalCo2Saved { get; set; }
    }
}
