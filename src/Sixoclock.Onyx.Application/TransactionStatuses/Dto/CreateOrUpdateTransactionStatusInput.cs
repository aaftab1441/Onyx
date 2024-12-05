using Abp.AutoMapper;

namespace Sixoclock.Onyx.TransactionStatuses.Dto
{
    [AutoMapTo(typeof(TransactionStatus))]
    public class CreateOrUpdateTransactionStatusInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
    }
}
