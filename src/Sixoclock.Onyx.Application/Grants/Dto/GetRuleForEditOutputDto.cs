using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sixoclock.Onyx.Grants.Dto
{
    public class GetRuleForEditOutputDto
    {
        public RuleDto Rule { get; set; }
        public IEnumerable<ComboboxItemDto> RuleOperators { get; set; }
        public IEnumerable<ComboboxItemDto> RuleStartRelations { get; set; }
        public IEnumerable<ComboboxItemDto> RuleEndRelations { get; set; }
        public IEnumerable<ComboboxItemDto> Entities { get; set; }
        public IEnumerable<PropertyDto> Properties { get; set; }
    }
}
