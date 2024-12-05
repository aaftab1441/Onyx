using System;

namespace Sixoclock.Onyx.TagTransactions.Dto
{
    public class TransactionListDto
    {
        public DateTime? Date { get; set; }
        public string Duration { get; set; }
        public float? Kwh { get; set; }
        public string Installation { get; set; }
        public string Group { get; set; }
        public string Charger { get; set; }
        public int? EVSE { get; set; }
        public string User { get; set; }
        public float? BillKwh { get; set; }
        public float? BillMin { get; set; }
        public float? Cost { get; set; }
        public float? Earned { get; set; }
        public float? ToBilled { get; set; }
        public DateTime? StopDate { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
