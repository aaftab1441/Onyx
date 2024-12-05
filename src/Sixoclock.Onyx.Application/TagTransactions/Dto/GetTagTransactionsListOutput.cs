using System.Collections.Generic;

namespace Sixoclock.Onyx.TagTransactions.Dto
{
    public class GetTagTransactionsListOutput
    {
        public IEnumerable<TagTransactionListDto> TagTransactions { get; set; }
    }
}
