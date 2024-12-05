using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.Grants.Dto
{
    [AutoMapFrom(typeof(RuleSet))]
    public class RuleSetDto:FullAuditedEntityDto
    {
        public string Name { get; set; }
    }
}
