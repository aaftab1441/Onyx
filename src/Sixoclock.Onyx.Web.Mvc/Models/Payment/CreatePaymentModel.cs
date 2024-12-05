using Sixoclock.Onyx.Editions;
using Sixoclock.Onyx.MultiTenancy.Payments;

namespace Sixoclock.Onyx.Web.Models.Payment
{
    public class CreatePaymentModel
    {
        public int EditionId { get; set; }

        public PaymentPeriodType? PaymentPeriodType { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}
