using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx.UserGrants.Dto
{
    [AutoMapFrom(typeof(UserRuleSet))]
    [AutoMapTo(typeof(UserRuleSet))]
    public class UserRuleSetInput
    {
        public long UserId { get; set; }
        public int? RuleSetId { get; set; }
    }
}
