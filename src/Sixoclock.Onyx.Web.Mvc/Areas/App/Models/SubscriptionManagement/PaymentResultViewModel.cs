using Abp.AutoMapper;
using Sixoclock.Onyx.Editions;
using Sixoclock.Onyx.MultiTenancy.Payments.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.SubscriptionManagement
{
    [AutoMapTo(typeof(ExecutePaymentDto))]
    public class PaymentResultViewModel : SubscriptionPaymentDto
    {
        public EditionPaymentType EditionPaymentType { get; set; }
    }
}