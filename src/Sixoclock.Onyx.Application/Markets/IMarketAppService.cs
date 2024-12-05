using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Markets.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Markets
{
    public interface IMarketAppService : IApplicationService
    {
        Task CreateOrUpdateMarket(CreateOrUpdateMarketInput input);
        Task DeleteMarket(EntityDto<int> input);
        Task<PagedResultDto<MarketDto>> GetMarket(GetMarketInput input);
        Task<GetMarketForEditOutput> GetMarketForEdit(EntityDto<int> input);
        Task<GetMarketsListOutput> GetMarketsList();
        Task<GetMarketsListOutput> GetMarketsListByClient(EntityDto<int> input);
    }
}