using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Vendors;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.MeterTypes;
using Sixoclock.Onyx.EVSEs;
using Sixoclock.Onyx.Chargepoints;
using Sixoclock.Onyx.EVSEs.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.EVSEs;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class EVSEsController : OnyxControllerBase
    {
        private readonly IEVSEAppService _eVSEAppService;
        private readonly IVendorAppService _vendorAppService;
        private readonly IChargepointAppService _chargePointAppService;
        private readonly IMeterTypeAppService _meterTypeAppService;

        public EVSEsController(IEVSEAppService eVSEAppService,IVendorAppService vendorAppService,IChargepointAppService chargePointAppService,IMeterTypeAppService meterTypeAppService)
        {
            _eVSEAppService = eVSEAppService;
            _vendorAppService = vendorAppService;
            _chargePointAppService = chargePointAppService;
            _meterTypeAppService = meterTypeAppService;
        }
        public async Task<ActionResult> Index(GetEVSEInput input)
        {
            var output = await _eVSEAppService.GetEVSE(input);
            var model = new EVSEsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditEVSEModal(EntityDto<int> input)
        {
            var output = _eVSEAppService.GetEVSEForEdit(input);
            var viewModel = new CreateOrEditEVSEViewModel(output);

            ViewBag.Vendors = new SelectList(_vendorAppService.GetVendorsList().Vendors, "Id", "Name");
            ViewBag.Chargepoints = new SelectList((await _chargePointAppService.GetChargepointsList()).Chargepoints, "Id", "Name");
            ViewBag.MeterTypes = new SelectList(_meterTypeAppService.GetMeterTypesList().MeterTypes, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}