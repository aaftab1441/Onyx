using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Markets.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;
using Sixoclock.Onyx.Markets.Exporting;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;
using Sixoclock.Onyx.TagTransactions;

namespace Sixoclock.Onyx.Markets
{
    public class MarketAppService : OnyxAppServiceBase, IMarketAppService
    {
        private readonly IRepository<Market> _marketRepository;
        private readonly IMarketRuleSetExpressionBuilder _ruleSetExpressionBuilder;
        private readonly IMarketListExcelExporter _marketListExcelExporter;
        private readonly ITagTransactionAppService _tagTransactionAppService;

        public MarketAppService(IRepository<Market> marketRepository, 
            IMarketRuleSetExpressionBuilder ruleSetExpressionBuilder,
            IMarketListExcelExporter marketListExcelExporter,
            ITagTransactionAppService tagTransactionAppService)
        {
            _marketRepository = marketRepository;
            _ruleSetExpressionBuilder = ruleSetExpressionBuilder;
            _marketListExcelExporter = marketListExcelExporter;
            _tagTransactionAppService = tagTransactionAppService;
        }

        public async Task CreateOrUpdateMarket(CreateOrUpdateMarketInput input)
        {
            var market = ObjectMapper.Map<Market>(input);
            
            if (input.Id == 0)
            {
                await _marketRepository.InsertAsync(market);
            }
            else
            {
                await _marketRepository.UpdateAsync(market);
            }
        }

        public async Task<GetMarketForEditOutput> GetMarketForEdit(EntityDto<int> input)
        {
            //Editing an existing market
            var output = new GetMarketForEditOutput();
            if (input.Id == 0)
            {
                output.Market = new MarketDto();
            }
            else
            {
                var market = await _marketRepository.GetAsync(input.Id);

                output.Market = ObjectMapper.Map<MarketDto>(market);
            }

            return output;
        }

        public async Task<PagedResultDto<MarketDto>> GetMarket(GetMarketInput input)
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            var query = (from market in _marketRepository.GetAll().AsExpandable().Include(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(expressions).Include(c => c.Customer)
                         select new MarketDto
                         {
                             Id = market.Id,
                             TenantId = market.TenantId,
                             MarketName = market.MarketName,
                             CustomerId = market.CustomerId,
                             CustomerName = market.Customer.CustomerName,
                             CreationTime = market.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.MarketName.Contains(input.Filter) || item.CustomerName.Contains(input.Filter))
                .WhereIf(!input.MarketName.IsNullOrWhiteSpace(), item => item.MarketName == input.MarketName)
                .WhereIf(!input.ClientName.IsNullOrWhiteSpace(), item => item.CustomerName == input.ClientName)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<MarketDto>(resultCount, results.ToList());
        }

        public async Task<GetMarketsListOutput> GetMarketsList()
        {
            var expressions =await  _ruleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<MarketListDto> _marketsList = from markets in _marketRepository.GetAll().AsExpandable().Include(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(expressions)
                                                      select new MarketListDto
                                                      {
                                                          Id = markets.Id,
                                                          Name = markets.MarketName
                                                      };
            return new GetMarketsListOutput { Markets = _marketsList.ToList() };
        }
        public async Task<GetMarketsListOutput> GetMarketsListByClient(EntityDto<int> input)
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<MarketListDto> _marketsList = from markets in _marketRepository.GetAll().AsExpandable().Include(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(f => f.CustomerId == input.Id).Where(expressions)
                                                         select new MarketListDto
                                                         {
                                                             Id = markets.Id,
                                                             Name = markets.MarketName
                                                         };
            return new GetMarketsListOutput { Markets = _marketsList.ToList() };
        }
        public FileDto GetMarketsToExcel(EntityDto<int> input)
        {
            var markets = _tagTransactionAppService.GetTransactionsUtilisationTotalByMarket(input).TagTransactions.ToList();
            
            return _marketListExcelExporter.ExportToFile(markets);
        }
        public async Task DeleteMarket(EntityDto<int> input)
        {
            var market = await _marketRepository.GetAsync(input.Id);
            await _marketRepository.DeleteAsync(market);
        }
    }
}
