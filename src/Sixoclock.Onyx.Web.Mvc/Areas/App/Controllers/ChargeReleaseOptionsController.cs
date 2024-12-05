using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ChargeReleaseOptions;
using Sixoclock.Onyx.ChargeReleaseOptions.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ChargeReleaseOptions;
using Abp.Application.Services.Dto;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ChargeReleaseOptionsController : OnyxControllerBase
    {
        private readonly IChargeReleaseOptionAppService _chargeReleaseOptionAppService;
        
        public ChargeReleaseOptionsController(IChargeReleaseOptionAppService _chargeReleaseOptionAppService)
        {
            this._chargeReleaseOptionAppService = _chargeReleaseOptionAppService;
        }
        public async Task<ActionResult> Index(GetChargeReleaseOptionInput input)
        {
            var output = await _chargeReleaseOptionAppService.GetChargeReleaseOption(input);
            var model = new ChargeReleaseOptionsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditChargeReleaseOptionModal(EntityDto<int> input)
        {
            var output = await _chargeReleaseOptionAppService.GetChargeReleaseOptionForEdit(input);
            var viewModel = new CreateOrEditChargeReleaseOptionViewModel(output);
            
            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}