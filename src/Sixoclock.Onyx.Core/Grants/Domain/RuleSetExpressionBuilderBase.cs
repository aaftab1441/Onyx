using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Authorization.Users;

namespace Sixoclock.Onyx.Grants.Domain
{
    public class RuleSetExpressionBuilderBase:OnyxDomainServiceBase,IRuleSetExpressionBuilder
    {
        private readonly IRepository<Rule> _ruleRepository;
        private readonly IRepository<UserRuleSet> _userRuleSetRepository;
        private readonly IAbpSession _abpSession;
        private readonly IRuleSetEntitiesProvider _ruleSetEntitiesProvider;
        private readonly UserManager _userManager;


        public RuleSetExpressionBuilderBase(IRepository<UserRuleSet> userRuleSetRepository, IRepository<Rule> ruleRepository, IAbpSession abpSession, IRuleSetEntitiesProvider ruleSetEntitiesProvider, UserManager userManager)
        {
            _userRuleSetRepository = userRuleSetRepository;
            _ruleRepository = ruleRepository;
            _abpSession = abpSession;
            _ruleSetEntitiesProvider = ruleSetEntitiesProvider;
            _userManager = userManager;
        }


        public  Rule GetRuleEnd(List<Rule> rules, int i)
        {
            int paranthesisStartCount = 0;
            int paranthesisEndCount = 0;
            for (int count = i; count < rules.Count; count++)
            {
                if (rules[count].IsParanthesisStart)
                {
                    paranthesisStartCount++;
                }
                if (rules[count].RuleRelation.Value.Contains(")"))
                {
                    paranthesisEndCount++;
                }
                if (paranthesisStartCount == paranthesisEndCount && rules[count].RuleRelation.Value.Contains(")"))
                {
                    return rules[count];
                }
            }
            return rules.GetRange(i, rules.Count - i).FirstOrDefault(x=>x.RuleRelation.Value.Contains(")"));
        }

        public Task<ExpressionStarter<T>> BuiExpressionTree<T>()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetVariableRuleValueAsync(string value)
        {
            string updatedvalue = string.Empty;
            if (value.ToLower() == "[role]")
            {
                var userId=_abpSession.GetUserId();
                await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId.ToString()));

            }
            else if (value.ToLower() == "[username]")
            {
                var userId = _abpSession.GetUserId();
                var user=await _userManager.FindByIdAsync(userId.ToString());
                return user.UserName;
            }
            else
            {
                updatedvalue = value;
            }
            return updatedvalue;
        }


        public async Task<List<RuleSet>> GetAllRuleSets()
        {
            var userId = _abpSession.GetUserId();
            var ruleSets = await _userRuleSetRepository.GetAll().Where(x => x.UserId == userId).Include(x => x.RuleSet)
                .Select(x => x.RuleSet).ToListAsync();
            foreach (var ruleSet in ruleSets)
            {
                ruleSet.Rules = await _ruleRepository.GetAll().Where(x => x.RuleSetId == ruleSet.Id)
                    .Include(x => x.RuleCondition).Include(x => x.RuleRelation).OrderBy(x => x.Order).ToListAsync();
            }
            return ruleSets;
        }

    }
}
