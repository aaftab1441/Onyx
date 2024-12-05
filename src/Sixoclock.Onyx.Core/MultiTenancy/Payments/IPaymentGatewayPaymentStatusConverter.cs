namespace Sixoclock.Onyx.MultiTenancy.Payments
{
    public interface IPaymentGatewayPaymentStatusConverter
    {
        SubscriptionPaymentStatus ConvertToSubscriptionPaymentStatus(string externalStatus);
    }
}