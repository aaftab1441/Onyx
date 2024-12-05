using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx.Grants.Dto
{
    [AutoMapFrom(typeof(RuleSet))]
    public class RuleSetListDto:FullAuditedEntityDto
    {
        public string Name { get; set; }
        public string Translation { get; set; }
        public int RuleCount { get; set; }
    }
}
