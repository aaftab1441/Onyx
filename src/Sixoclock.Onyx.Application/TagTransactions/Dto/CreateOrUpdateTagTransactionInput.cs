using Abp.AutoMapper;
using System;

namespace Sixoclock.Onyx.TagTransactions.Dto
{
    [AutoMapTo(typeof(TagTransaction))]
    public class CreateOrUpdateTagTransactionInput
    {
        public int Id { get; set; }
        public DateTime TagTransactioTime { get; set; }

        public int TagId { get; set; }
        public int TagTransactionTypeId { get; set; }
        public int TransactionId { get; set; }
        public int TenantId { get; set; }
    }
}
