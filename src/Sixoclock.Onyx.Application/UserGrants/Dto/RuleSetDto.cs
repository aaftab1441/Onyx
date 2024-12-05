using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx.UserGrants.Dto
{
    [AutoMapFrom(typeof(RuleSet))]
    public class RuleSetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
