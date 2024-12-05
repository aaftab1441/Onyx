using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Grants.Dto
{
    public class GetRuleSetListOutput
    {
        public IEnumerable<RuleSetListDto> RuleSets { get; set; }
    }
}
