using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Domain.Services;

namespace Sixoclock.Onyx.Grants.Domain
{
    public class GrantRuleValidator : DomainService,IRuleValidator
    {
        public string Translate(List<Rule> rules)
        {

            return string.Empty;
        }

        public bool Validate(List<Rule> rules)
        {
            //var rules = ruleSet.Rules.OrderBy(x => x.Order);
            //for (int i = 0; i < rules.Count(); i++)
            //{


            //}
            return true;
        }
    }
}
