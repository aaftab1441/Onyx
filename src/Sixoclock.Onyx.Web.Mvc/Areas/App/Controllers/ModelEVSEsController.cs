using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ModelEVSEs;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ModelEVSEs.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ModelEVSEs;
using Sixoclock.Onyx.ChargePointModels;
using Sixoclock.Onyx.Vendors;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.MeterTypes;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ModelEVSEsController : OnyxControllerBase
    {
        private readonly IModelEVSEAppService _modelEVSEAppService;
        private readonly IVendorAppService _vendorAppService;
        private readonly IChargePointModelAppService _chargePointModelAppService;
        private readonly IMeterTypeAppService _meterTypeAppService;

        public ModelEVSEsController(IModelEVSEAppService modelEVSEAppService,IVendorAppService vendorAppService,IChargePointModelAppService chargePointModelAppService,IMeterTypeAppService meterTypeAppService)
        {
            _modelEVSEAppService = modelEVSEAppService;
            _vendorAppService = vendorAppService;
            _chargePointModelAppService = chargePointModelAppService;
            _meterTypeAppService = meterTypeAppService;
        }
        public async Task<ActionResult> Index(GetModelEVSEInput input)
        {
            var output = await _modelEVSEAppService.GetModelEVSE(input);
            var model = new ModelEVSEsViewModel(output);

            return View(model);
        }
        public PartialViewResult CreateOrEditModelEVSEModal(EntityDto<int> input)
        {
            var output = _modelEVSEAppService.GetModelEVSEForEdit(input);
            var viewModel = new CreateOrEditModelEVSEViewModel(output);

            ViewBag.Vendors = new SelectList(_vendorAppService.GetVendorsList().Vendors, "Id", "Name");
            ViewBag.ChargepointModels = new SelectList(_chargePointModelAppService.GetChargepointModelsList().ChargepointModels, "Id", "Name");
            ViewBag.MeterTypes = new SelectList(_meterTypeAppService.GetMeterTypesList().MeterTypes, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}