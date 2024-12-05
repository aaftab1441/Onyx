using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.Grants.Dto
{
    [AutoMapFrom(typeof(Rule))]
    [AutoMapTo(typeof(Rule))]
    public class RuleDto:FullAuditedEntityDto
    {
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public int RuleConditionId { get; set; }
        public int? RuleStartRelationId { get; set; }
        public int? RuleEndRelationId { get; set; }
        public int RuleSetId { get; set; }
        

    }
}
