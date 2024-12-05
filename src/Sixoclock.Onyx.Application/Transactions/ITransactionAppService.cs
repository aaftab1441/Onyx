using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Transactions.Dto;

namespace Sixoclock.Onyx.Transactions
{
    public interface ITransactionAppService
    {
        Task<PagedResultDto<TransactionDto>> GetUserTransactions(GetTransactionInput input);
        Task<PagedResultDto<TransactionDto>> GetTenantTransactions(GetTransactionInput input);
        Task<PagedResultDto<TransactionDto>> GetTransactionsByChargepoint(GetTransactionsByChargepointInput input);
    }
}