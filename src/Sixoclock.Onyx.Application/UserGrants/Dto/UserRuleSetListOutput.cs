using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.UserGrants.Dto
{
    public class UserRuleSetListOutput
    {
        public IEnumerable<UserRuleSetListDto> UserRuleSets { get; set; }
    }
}
