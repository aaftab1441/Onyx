using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Grants.Dto;

namespace Sixoclock.Onyx
{
    public interface IRuleSetService: IApplicationService
    {
        Task<GetRuleSetForEditOutput> GetRuleSetForEdit(EntityDto<int> input);
        Task<GetRuleSetListOutput> GetRuleSets();
        Task CreateOrUpdateRuleSet(CreateOrUpdateRuleSetInputDto input);
        Task DeleteRuleSet(EntityDto<int> input);

    }
}
