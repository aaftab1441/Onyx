using System.Collections.Generic;

namespace Sixoclock.Onyx.TransactionStatuses.Dto
{
    public class GetTransactionStatusesListOutput
    {
        public IEnumerable<TransactionStatusListDto> TransactionStatuses { get; set; }
    }
}
