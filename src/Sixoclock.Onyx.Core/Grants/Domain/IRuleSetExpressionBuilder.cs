using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using LinqKit;

namespace Sixoclock.Onyx.Grants.Domain
{
    public interface IRuleSetExpressionBuilder:IDomainService
    {
        Task<List<RuleSet>> GetAllRuleSets();
        Rule GetRuleEnd(List<Rule> rules, int i);

        Task<ExpressionStarter<T>> BuiExpressionTree<T>();
        Task<string> GetVariableRuleValueAsync(string value);
    }
}
