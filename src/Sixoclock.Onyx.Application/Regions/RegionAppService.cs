using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Regions.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;
using Sixoclock.Onyx.Regions.Exporting;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Regions
{
    public class RegionAppService : OnyxAppServiceBase, IRegionAppService
    {
        private readonly IRepository<Region> _regionRepository;
        private readonly IRegionRuleSetExpressionBuilder _ruleSetExpressionBuilder;
        private readonly IRegionListExcelExporter _regionListExcelExporter;
        private readonly ITagTransactionAppService _tagTransactionAppService;

        public RegionAppService(IRepository<Region> regionRepository, 
            IRegionRuleSetExpressionBuilder ruleSetExpressionBuilder,
            IRegionListExcelExporter regionListExcelExporter,
            ITagTransactionAppService tagTransactionAppService)
        {
            _regionRepository = regionRepository;
            _ruleSetExpressionBuilder = ruleSetExpressionBuilder;
            _regionListExcelExporter = regionListExcelExporter;
            _tagTransactionAppService = tagTransactionAppService;
        }

        public async Task CreateOrUpdateRegion(CreateOrUpdateRegionInput input)
        {
            var region = ObjectMapper.Map<Region>(input);

            if (input.Id == 0)
            {
                await _regionRepository.InsertAsync(region);
            }
            else
            {
                await _regionRepository.UpdateAsync(region);
            }
        }

        public async Task<GetRegionForEditOutput> GetRegionForEdit(EntityDto<int> input)
        {
            //Editing an existing region
            var output = new GetRegionForEditOutput();
            if (input.Id == 0)
            {
                output.Region = new RegionDto();
            }
            else
            {
                var region = await _regionRepository.GetAsync(input.Id);

                output.Region = ObjectMapper.Map<RegionDto>(region);
            }

            return output;
        }

        public async Task<PagedResultDto<RegionDto>> GetRegion(GetRegionInput input)
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            var query = (from region in _regionRepository.GetAll().AsExpandable().Include(x => x.Market)
                    .ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(expressions)
                select new RegionDto
                {
                    Id = region.Id,
                    RegionName = region.RegionName,
                    MarketId = region.MarketId,
                    MarketName = region.Market.MarketName,
                    CreationTime = region.CreationTime
                }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.RegionName.Contains(input.Filter) || item.MarketName.Contains(input.Filter))
                .WhereIf(!input.RegionName.IsNullOrWhiteSpace(), item => item.RegionName == input.RegionName)
                .WhereIf(!input.MarketName.IsNullOrWhiteSpace(), item => item.MarketName == input.MarketName)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<RegionDto>(resultCount, results.ToList());
        }

        public async Task DeleteRegion(EntityDto<int> input)
        {
            var region = await _regionRepository.GetAsync(input.Id);
            await _regionRepository.DeleteAsync(region);
        }

        public async Task<GetRegionsListOutput> GetRegionsList()
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<RegionListDto> _regionsList = from region in _regionRepository.GetAll().AsExpandable().Include(x => x.Market).ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(expressions)
                                                         select new RegionListDto
                                                         {
                                                             Id = region.Id,
                                                             Name = region.RegionName
                                                         };
            return new GetRegionsListOutput { Regions = _regionsList.ToList() };
        }
        public FileDto GetRegionsToExcel(EntityDto<int> input)
        {
            var regions = _tagTransactionAppService.GetTransactionsUtilisationTotalByRegion(input).TagTransactions.ToList();
            
            return _regionListExcelExporter.ExportToFile(regions);
        }
        public async Task<GetRegionsListOutput> GetRegionsListByMarket(EntityDto<int> input)
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<RegionListDto> _regionsList = from region in _regionRepository.GetAll().AsExpandable()
                    .Include(x => x.Market).ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(expressions).Where(f => f.MarketId == input.Id)
                select new RegionListDto
                {
                    Id = region.Id,
                    Name = region.RegionName
                };
            return new GetRegionsListOutput { Regions = _regionsList.ToList() };
        }
    }
}
