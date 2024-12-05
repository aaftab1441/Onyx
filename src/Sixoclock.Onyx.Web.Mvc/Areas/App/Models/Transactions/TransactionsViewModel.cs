using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Transactions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Transactions
{
    [AutoMapFrom(typeof(ListResultDto<TransactionDto>))]
    public class TransactionsViewModel : ListResultDto<TransactionDto>
    {
        public TransactionsViewModel(ListResultDto<TransactionDto> output)
        {
            output.MapTo(this);
        }
    }
}
