using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Web.Areas.App.Models.Grants;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Grants)]
    public class GrantRulesController: OnyxControllerBase
    {
        private readonly IRuleService _ruleAppService;

        public GrantRulesController(IRuleService ruleAppService)
        {
            _ruleAppService = ruleAppService;
        }
        public PartialViewResult Index(string id)
        {
            ViewBag.RuleSetId = id;
            return PartialView();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Grants_Create, AppPermissions.Pages_Grants_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id, string ruleSetId)
        {
            var output = await _ruleAppService.GetRuleForEdit(new Grants.Dto.GetRuleForEditParamInput<int>{ Id = Convert.ToInt32(id), RuleSetId = Convert.ToInt32(ruleSetId) });
            var viewModel = new CreateOrEditGrantRuleModelViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);

        }
    }
}
