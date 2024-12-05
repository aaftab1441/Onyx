using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Installs.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;
using Sixoclock.Onyx.Installs.Exporting;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Installs
{
    public class InstallAppService : OnyxAppServiceBase, IInstallAppService
    {
        private readonly IRepository<Install> _installRepository;
        private readonly IInstallRuleSetExpressionBuilder _ruleSetExpressionBuilder;
        private readonly IInstallListExcelExporter _installListExcelExporter;
        private readonly ITagTransactionAppService _tagTransactionAppService;

        public InstallAppService(IRepository<Install> installRepository, 
            IInstallRuleSetExpressionBuilder ruleSetExpressionBuilder,
            IInstallListExcelExporter installListExcelExporter,
            ITagTransactionAppService tagTransactionAppService)
        {
            _installRepository = installRepository;
            _ruleSetExpressionBuilder = ruleSetExpressionBuilder;
            _installListExcelExporter = installListExcelExporter;
            _tagTransactionAppService = tagTransactionAppService;
        }
        public async Task CreateOrUpdateInstall(CreateOrUpdateInstallInput input)
        {
            var install = ObjectMapper.Map<Install>(input);

            if (input.Id == 0)
            {
                await _installRepository.InsertAsync(install);
            }
            else
            {
                await _installRepository.UpdateAsync(install);
            }
        }
        public async Task<GetInstallForEditOutput> GetInstallForEdit(EntityDto<int> input)
        {
            //Editing an existing install
            var output = new GetInstallForEditOutput();
            if (input.Id == 0)
            {
                output.Install = new InstallDto();
            }
            else
            {
                var install = await _installRepository.GetAsync(input.Id);

                output.Install = ObjectMapper.Map<InstallDto>(install);
            }

            return output;
        }

        public async Task<PagedResultDto<InstallDto>> GetInstall(GetInstallInput input)
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            var query = (from install in _installRepository.GetAll().AsExpandable()
                    .Include(x => x.Region).ThenInclude(x => x.Market).ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(expressions)
                select new InstallDto
                {
                    Id = install.Id,
                    InstallName = install.InstallName,
                    RegionId = install.RegionId,
                    RegionName = install.Region.RegionName,
                    Latitude = install.Latitude,
                    Longitude = install.Longitude,
                    CreationTime = install.CreationTime
                }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.InstallName.Contains(input.Filter) || item.RegionName.Contains(input.Filter))
                .WhereIf(!input.InstallName.IsNullOrWhiteSpace(), item => item.InstallName == input.InstallName)
                .WhereIf(!input.RegionName.IsNullOrWhiteSpace(), item => item.RegionName == input.RegionName)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<InstallDto>(resultCount, results.ToList());
        }
        public async Task<GetInstallsListOutput> GetInstallsList()
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<InstallListDto> _installsList = from install in _installRepository.GetAll().AsExpandable()
                    .Include(x => x.Region).ThenInclude(x => x.Market).ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(expressions)
                select new InstallListDto
                {
                    Id = install.Id,
                    Name = install.InstallName
                };
            return new GetInstallsListOutput { Installs = _installsList.ToList() };
        }
        public async Task<GetInstallsListOutput> GetInstallsListByRegion(EntityDto<int> input)
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<InstallListDto> _installsList = from install in _installRepository.GetAll().AsExpandable()
                    .Include(x => x.Region).ThenInclude(x => x.Market).ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(f => f.RegionId == input.Id)
                select new InstallListDto
                {
                    Id = install.Id,
                    Name = install.InstallName
                };
            return new GetInstallsListOutput { Installs = _installsList.ToList() };
        }
        public FileDto GetInstallsToExcel(EntityDto<int> input)
        {
            var installs = _tagTransactionAppService.GetTransactionsUtilisationTotalByInstall(input).TagTransactions.ToList();
            
            return _installListExcelExporter.ExportToFile(installs);
        }
        public async Task DeleteInstall(EntityDto<int> input)
        {
            var install = await _installRepository.GetAsync(input.Id);
            await _installRepository.DeleteAsync(install);
        }
    }
}
