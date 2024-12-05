using Abp.AutoMapper;
using Sixoclock.Onyx.MultiTenancy.Payments;

namespace Sixoclock.Onyx.Sessions.Dto
{
    [AutoMapFrom(typeof(SubscriptionPayment))]
    public class SubscriptionPaymentInfoDto
    {
        public decimal Amount { get; set; }
    }
}