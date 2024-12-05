using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Sixoclock.Onyx.Grants;
using Sixoclock.Onyx.Grants.Dto;
using Sixoclock.Onyx.Grants.Domain;

namespace Sixoclock.Onyx
{
    public class RuleSetService:OnyxAppServiceBase,IRuleSetService
    {
        private readonly IRepository<RuleSet> _ruleSetRepository;
        

        public RuleSetService(IRepository<RuleSet> ruleSetRepository)
        {
            _ruleSetRepository = ruleSetRepository;
          
        }

        public async Task<GetRuleSetForEditOutput> GetRuleSetForEdit(EntityDto<int> input)
        {
            if (input.Id > 0)
            {
                var ruleSetDto = await _ruleSetRepository.GetAll().Where(x => x.Id == input.Id).Include(x => x.Rules).FirstOrDefaultAsync();
                var ruleSet = new GetRuleSetForEditOutput() { RuleSet = ObjectMapper.Map<RuleSetDto>(ruleSetDto) };
                return ruleSet;
            }
            else
            {
                var ruleSet = new GetRuleSetForEditOutput() { RuleSet = new RuleSetDto()};
                return ruleSet;
            }
        }

        public async Task<GetRuleSetListOutput> GetRuleSets()
        {
            var rulsetsEntities = await _ruleSetRepository.GetAll().Include(x=>x.Rules).ToListAsync();
            var ruleset = rulsetsEntities.Select(x => new RuleSetListDto()
            {
                Id = x.Id,
                Name = x.Name,
                RuleCount = x.Rules.Count,
                Translation = string.Empty
            });
            return new GetRuleSetListOutput() { RuleSets = ruleset};

        }

        public async Task CreateOrUpdateRuleSet(CreateOrUpdateRuleSetInputDto input)
        {

            
                
                if (input.RuleSet.Id > 0)
                {
                    var ruleSetModel = await _ruleSetRepository.GetAsync(input.RuleSet.Id);
                    ruleSetModel.Name = input.RuleSet.Name;
                    await _ruleSetRepository.UpdateAsync(ruleSetModel);
                }
                else
                {
                    var ruleSetModel = new RuleSet() { Id = input.RuleSet.Id, Name = input.RuleSet.Name };
                    await _ruleSetRepository.InsertAsync(ruleSetModel);

                }
            
        }

        public async Task DeleteRuleSet(EntityDto<int> input)
        {
            await _ruleSetRepository.DeleteAsync(input.Id);
        }
    }
}
