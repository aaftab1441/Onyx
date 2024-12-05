using Sixoclock.Onyx.Editions.Dto;

namespace Sixoclock.Onyx.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }
    }
}
