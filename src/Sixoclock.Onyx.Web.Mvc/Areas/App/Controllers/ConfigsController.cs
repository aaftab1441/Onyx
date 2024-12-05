using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Configs;
using Sixoclock.Onyx.Configs.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Configs;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.OCPPFeatures;
using Sixoclock.Onyx.OCPPVersions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ConfigsController : OnyxControllerBase
    {
        private readonly IConfigAppService _configAppService;
        private readonly IOCPPVersionAppService _oCPPVersionService;

        public ConfigsController(IConfigAppService configAppService, IOCPPVersionAppService oCPPVersionService)
        {
            _configAppService = configAppService;
            _oCPPVersionService = oCPPVersionService;
        }
        public async Task<ActionResult> Index(GetConfigInput input)
        {
            var output = await _configAppService.GetConfig(input);
            var model = new ConfigsViewModel(output);
            
            ViewBag.Versions = new SelectList(_oCPPVersionService.GetOCPPVersionsList().OCPPVersions, "Id", "Name");

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditConfigModal(EntityDto<int> input)
        {
            var output = await _configAppService.GetConfigForEdit(input);
            var viewModel = new CreateOrEditConfigViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}