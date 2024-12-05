using System.Collections.Generic;

namespace Sixoclock.Onyx.TagTransactions.Dto
{
    public class GetTagTransactionsDownloadListOutput
    {
        public IEnumerable<TagTransactionDownloadListDto> TagTransactions { get; set; }
    }
}
