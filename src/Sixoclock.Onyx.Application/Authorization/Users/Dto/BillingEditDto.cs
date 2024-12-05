namespace Sixoclock.Onyx.Authorization.Users.Dto
{
    public class BillingEditDto
    {
        public float? CostkWh { get; set; }
        public float? CostMin { get; set; }
        public float? TotalkWh { get; set; }
        public int? TotalSessions { get; set; }
        public string Comment { get; set; }
        public int? CurrencyId { get; set; }
        public int? BillingTypeId { get; set; }
    }
}
