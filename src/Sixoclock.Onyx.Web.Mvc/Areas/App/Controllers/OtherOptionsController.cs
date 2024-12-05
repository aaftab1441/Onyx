using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.OtherOptions;
using Sixoclock.Onyx.OtherOptions.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.OtherOptions;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class OtherOptionsController : OnyxControllerBase
    {
        private readonly IOtherOptionAppService _otherOptionAppService;

        public OtherOptionsController(IOtherOptionAppService _otherOptionAppService)
        {
            this._otherOptionAppService = _otherOptionAppService;
        }
        public async Task<ActionResult> Index(GetOtherOptionInput input)
        {
            var output = await _otherOptionAppService.GetOtherOption(input);
            var model = new OtherOptionsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditOtherOptionModal(EntityDto<int> input)
        {
            var output = await _otherOptionAppService.GetOtherOptionForEdit(input);
            var viewModel = new CreateOrEditOtherOptionViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}