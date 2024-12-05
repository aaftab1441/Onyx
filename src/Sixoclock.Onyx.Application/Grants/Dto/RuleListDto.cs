using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sixoclock.Onyx.Grants.Dto
{
    public class RuleListDto:FullAuditedEntityDto
    {
        public string RelationStart { get; set; }
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public string RuleCondition { get; set; }
        public string Value { get; set; }
        public string RelationEnd { get; set; }
       
    }
}
