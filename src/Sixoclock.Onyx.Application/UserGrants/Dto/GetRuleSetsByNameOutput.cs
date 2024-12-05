using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sixoclock.Onyx.UserGrants.Dto
{
    public class GetRuleSetsByNameOutput
    {
        public IEnumerable<RuleSetDto> RuleSets { get; set; }
    }
}
