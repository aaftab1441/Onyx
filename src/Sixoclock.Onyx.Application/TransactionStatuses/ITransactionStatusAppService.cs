using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.TransactionStatuses.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.TransactionStatuses
{
    public interface ITransactionStatusAppService : IApplicationService
    {
        Task CreateOrUpdateTransactionStatus(CreateOrUpdateTransactionStatusInput input);
        Task DeleteTransactionStatus(EntityDto<int> input);
        ListResultDto<TransactionStatusDto> GetTransactionStatus(GetTransactionStatusInput input);
        Task<GetTransactionStatusForEditOutput> GetTransactionStatusForEdit(EntityDto<int> input);
        GetTransactionStatusesListOutput GetTransactionStatusesList();
    }
}