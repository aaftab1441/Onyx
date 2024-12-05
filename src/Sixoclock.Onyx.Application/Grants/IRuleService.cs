using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Grants.Dto;

namespace Sixoclock.Onyx
{
    public interface IRuleService:IApplicationService
    {
        Task<GetRuleForEditOutputDto> GetRuleForEdit(GetRuleForEditParamInput<int> input);
        Task<GetRuleListOutput> GetRules(int ruleSetId);
        Task CreateOrUpdateRule(CreateOrUpdateRuleInputDto input);
        Task DeleteRule(EntityDto<int> input);
        Task<List<PropertyDto>> GetProperties(string entityName);
        Task<List<ComboboxItemDto>> GetPropertyValues(string propertyType);
    }
}
