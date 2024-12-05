using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.Grants.Dto
{
    
    public class GetRuleSetForEditOutput
    {
        public RuleSetDto RuleSet { get; set; }
    }
}
