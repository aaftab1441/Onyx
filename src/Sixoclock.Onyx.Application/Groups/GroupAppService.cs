using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Groups.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;
using Sixoclock.Onyx.Groups.Exporting;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Groups
{
    public class GroupAppService : OnyxAppServiceBase, IGroupAppService
    {
        private readonly IRepository<Group> _groupRepository;
        private readonly IGroupRuleSetExpressionBuilder _ruleSetExpressionBuilder;
        private readonly IGroupListExcelExporter _groupListExcelExporter;
        private readonly ITagTransactionAppService _tagTransactionAppService;

        public GroupAppService(IRepository<Group> groupRepository, 
            IGroupRuleSetExpressionBuilder ruleSetExpressionBuilder,
            IGroupListExcelExporter groupListExcelExporter,
            ITagTransactionAppService tagTransactionAppService)
        {
            _groupRepository = groupRepository;
            _ruleSetExpressionBuilder = ruleSetExpressionBuilder;
            _groupListExcelExporter = groupListExcelExporter;
            _tagTransactionAppService = tagTransactionAppService;
        }
        public async Task CreateOrUpdateGroup(CreateOrUpdateGroupInput input)
        {
            var group = ObjectMapper.Map<Group>(input);

            if (input.Id == 0)
            {
                await _groupRepository.InsertAsync(group);
            }
            else
            {
                await _groupRepository.UpdateAsync(group);
            }
        }
        public async Task<GetGroupForEditOutput> GetGroupForEdit(EntityDto<int> input)
        {
            //Editing an existing group
            var output = new GetGroupForEditOutput();
            if (input.Id == 0)
            {
                output.Group = new GroupDto();
            }
            else
            {
                var group = await _groupRepository.GetAsync(input.Id);

                output.Group = ObjectMapper.Map<GroupDto>(group);
            }

            return output;
        }

        public async Task<GetGroupsListOutput> GetGroupsList()
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<GroupListDto> _groupsList = from g in _groupRepository.GetAll().AsExpandable()
                    .Include(x => x.Install)
                    .ThenInclude(x => x.Region).ThenInclude(x => x.Market).ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(expressions)
                select new GroupListDto
                {
                    Id = g.Id,
                    Name = g.GroupName
                };
            return new GetGroupsListOutput { Groups = _groupsList.ToList() };
        }
        public async Task<GetGroupsListOutput> GetGroupsListByInstall(EntityDto<int> input)
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<GroupListDto> _groupsList = from g in _groupRepository.GetAll().AsExpandable()
                    .Include(x => x.Install)
                    .ThenInclude(x => x.Region).ThenInclude(x => x.Market).ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Segment).Where(f => f.InstallId == input.Id).Where(expressions)
                select new GroupListDto
                {
                    Id = g.Id,
                    Name = g.GroupName
                };
            return new GetGroupsListOutput { Groups = _groupsList.ToList() };
        }

        public async Task<PagedResultDto<GroupDto>> GetGroup(GetGroupInput input)
        {
            var expressions = await _ruleSetExpressionBuilder.BuiExpressionTree();
            var query = (from grp in _groupRepository.GetAll().AsExpandable().Include(x => x.Install).Include(x=>x.Country).Where(expressions)
                select new GroupDto
                {
                    Id = grp.Id,
                    GroupName = grp.GroupName,
                    InstallId = grp.InstallId,
                    InstallName = grp.Install.InstallName,
                    CountryId = grp.CountryId,
                    CountryName = grp.Country.Value,
                    CreationTime = grp.CreationTime
                }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.GroupName.Contains(input.Filter) || item.InstallName.Contains(input.Filter))
                .WhereIf(!input.GroupName.IsNullOrWhiteSpace(), item => item.GroupName == input.GroupName)
                .WhereIf(!input.InstallName.IsNullOrWhiteSpace(), item => item.InstallName == input.InstallName)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<GroupDto>(resultCount, results.ToList());
        }
        public FileDto GetGroupsToExcel(EntityDto<int> input)
        {
            var groups = _tagTransactionAppService.GetTransactionsUtilisationTotalByGroup(input).TagTransactions.ToList();
            
            return _groupListExcelExporter.ExportToFile(groups);
        }
        public async Task DeleteGroup(EntityDto<int> input)
        {
            var group = await _groupRepository.GetAsync(input.Id);
            await _groupRepository.DeleteAsync(group);
        }
    }
}
