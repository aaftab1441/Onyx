using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Grants;
using Sixoclock.Onyx.UserGrants.Dto;

namespace Sixoclock.Onyx
{
    public class UserRuleSetService:OnyxAppServiceBase,IUserRuleSetService
    {
        private readonly IRepository<UserRuleSet> _userRuleSetRepo;
        private readonly IRepository<RuleSet> _ruleSetRepo;

        public UserRuleSetService(IRepository<RuleSet> ruleSetRepo, IRepository<UserRuleSet> userRuleSetRepo)
        {
            _ruleSetRepo = ruleSetRepo;
            _userRuleSetRepo = userRuleSetRepo;
        }

        public Task<UserRuleSetListOutput> GetUserRuleSets(EntityDto<long> userId)
        {
            var userRulesets = _userRuleSetRepo.GetAll().Where(x => x.UserId == userId.Id).Include(x=>x.RuleSet).ThenInclude(u=>u.Rules).Select(x =>
                new UserRuleSetListDto()
                {
                    Id = x.Id,
                    RuleCount = x.RuleSet.Rules.Count,
                    Name = x.RuleSet.Name
                });
            return Task.FromResult(new UserRuleSetListOutput() {UserRuleSets = userRulesets.ToList()});
        }

        public async Task SetUserRuleSet(UserRuleSetInput userRuleSet)
        {
            var userRuleSetModel = ObjectMapper.Map<UserRuleSet>(userRuleSet);
            await _userRuleSetRepo.InsertAsync(userRuleSetModel);
        }

        public async Task<GetRuleSetsByNameOutput> GetRuleSetByName(string name)
       {
            var matchingRuleSets = _ruleSetRepo.GetAll().Where(x => x.Name.StartsWith(name)).Take(5);
            var ruleSetDtos = ObjectMapper.Map<IEnumerable<RuleSetDto>>(await matchingRuleSets.ToListAsync());
            return new GetRuleSetsByNameOutput() { RuleSets = ruleSetDtos };
        }

        public async Task DeleteUserRuleSet(EntityDto<int> id)
        {
            await _userRuleSetRepo.DeleteAsync(id.Id);
        }
    }
}
