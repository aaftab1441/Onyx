using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.MeterTypes;
using Sixoclock.Onyx.MeterTypes.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.MeterTypes;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class MeterTypesController : OnyxControllerBase
    {
        private readonly IMeterTypeAppService _meterTypeAppService;

        public MeterTypesController(IMeterTypeAppService meterTypeAppService)
        {
            _meterTypeAppService = meterTypeAppService;
        }
        public async Task<ActionResult> Index(GetMeterTypeInput input)
        {
            var output = await _meterTypeAppService.GetMeterType(input);
            var model = new MeterTypesViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditMeterTypeModal(EntityDto<int> input)
        {
            var output = await _meterTypeAppService.GetMeterTypeForEdit(input);
            var viewModel = new CreateOrEditMeterTypeViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}