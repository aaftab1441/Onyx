using Sixoclock.Onyx.Editions;
using Sixoclock.Onyx.Editions.Dto;
using Sixoclock.Onyx.Security;
using Sixoclock.Onyx.MultiTenancy.Payments;
using Sixoclock.Onyx.MultiTenancy.Payments.Dto;

namespace Sixoclock.Onyx.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType? Gateway { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public bool ShowPaymentExpireNotification()
        {
            return !string.IsNullOrEmpty(PaymentId);
        }
    }
}
