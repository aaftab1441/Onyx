using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Grants.Dto
{
    public class GetRuleForEditParamInput<T> : EntityDto<T>
    {
        public int RuleSetId { get; set; }
    }
}
