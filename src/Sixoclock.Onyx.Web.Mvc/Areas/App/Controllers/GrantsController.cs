using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Grants;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Grants)]
    public class GrantsController : OnyxControllerBase
    {
        //SegmentRule == Grant
        private readonly IRuleSetService _ruleSetAppService;

        public GrantsController(IRuleSetService ruleSetAppService)
        {
            _ruleSetAppService = ruleSetAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Grants_Create, AppPermissions.Pages_Grants_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            var output = await _ruleSetAppService.GetRuleSetForEdit(new EntityDto<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditGrantModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);

        }


    }
}