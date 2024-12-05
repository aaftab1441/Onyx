using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.TagTransactions
{
    public interface ITagTransactionAppService : IApplicationService
    {
        Task CreateOrUpdateTagTransaction(CreateOrUpdateTagTransactionInput input);
        Task DeleteTagTransaction(EntityDto<int> input);
        ListResultDto<TagTransactionDto> GetTagTransactionsByTransaction(EntityDto<int> input);
        Task<GetTagTransactionForEditOutput> GetTagTransactionForEdit(EntityDto<int> input);
        GetTagTransactionsListOutput GetTagTransactionsList();
        GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByGroup(EntityDto<int> input);
        GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByInstall(EntityDto<int> input);
        GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByRegion(EntityDto<int> input);
        GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByMarket(EntityDto<int> input);
        GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByCustomer(EntityDto<int> input);
        GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalBySegment(EntityDto<int> input);
        GetTransactionsOverviewOutput GetTransactionsDashboardByGroup(EntityDto<int> input);
        GetTransactionsOverviewOutput GetTransactionsDashboardByInstall(EntityDto<int> input);
        GetTransactionsOverviewOutput GetTransactionsDashboardByRegion(EntityDto<int> input);
        GetTransactionsOverviewOutput GetTransactionsDashboardByMarket(EntityDto<int> input);
        GetTransactionsOverviewOutput GetTransactionsDashboardByCustomer(EntityDto<int> input);
        GetTransactionsOverviewOutput GetTransactionsDashboardBySegment(EntityDto<int> input);
    }
}