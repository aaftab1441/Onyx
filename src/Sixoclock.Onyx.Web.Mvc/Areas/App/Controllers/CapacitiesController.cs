using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Capacities;
using Sixoclock.Onyx.Capacities.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Capacities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Units;
using Sixoclock.Onyx.Powers;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class CapacitiesController : OnyxControllerBase
    {
        private readonly ICapacityAppService _capacityAppService;
        private readonly IUnitAppService _unitAppService;
        private readonly IPowerAppService _powerAppService;

        public CapacitiesController(ICapacityAppService capacityAppService, IUnitAppService unitAppService, IPowerAppService powerAppService)
        {
            _capacityAppService = capacityAppService;
            _unitAppService = unitAppService;
            _powerAppService = powerAppService;
        }
        public async Task<ActionResult> Index(GetCapacityInput input)
        {
            var output = await _capacityAppService.GetCapacity(input);
            var model = new CapacitiesViewModel(output);

            ViewBag.Units = new SelectList(_unitAppService.GetUnitsList().Units, "Id", "Name");
            ViewBag.Powers = new SelectList(_powerAppService.GetPowersList().Powers, "Id", "Name");

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditCapacityModal(EntityDto<int> input)
        {
            var output = await _capacityAppService.GetCapacityForEdit(input);
            var viewModel = new CreateOrEditCapacitiesViewModel(output);

            ViewBag.Units = new SelectList(_unitAppService.GetUnitsList().Units, "Id", "Name");
            ViewBag.Powers = new SelectList(_powerAppService.GetPowersList().Powers, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}