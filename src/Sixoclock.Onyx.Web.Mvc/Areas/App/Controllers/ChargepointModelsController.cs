using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ChargePointModels;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ChargePointModels.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ChargepointModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Vendors;
using Sixoclock.Onyx.MountTypes;
using Sixoclock.Onyx.OCPPVersions;
using Sixoclock.Onyx.OCPPTransports;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ChargepointModelsController : OnyxControllerBase
    {
        private readonly IChargePointModelAppService _chargepointModelAppService;
        private readonly IVendorAppService _vendorAppService;
        private readonly IMountTypeAppService _mountTypeAppService;
        private readonly IOCPPVersionAppService _ocppVersionAppService;
        private readonly IOCPPTransportAppService _ocppTransportAppService;

        public ChargepointModelsController(IChargePointModelAppService chargepointModelAppService, IVendorAppService vendorAppService, IMountTypeAppService mountTypeAppService,
            IOCPPVersionAppService oCPPVersionAppService, IOCPPTransportAppService oCPPTransportAppService)
        {
            _chargepointModelAppService = chargepointModelAppService;
            _vendorAppService = vendorAppService;
            _mountTypeAppService = mountTypeAppService;
            _ocppVersionAppService = oCPPVersionAppService;
            _ocppTransportAppService = oCPPTransportAppService;
        }
        public async Task<ActionResult> Index(GetChargepointModelInput input)
        {
            var output = await _chargepointModelAppService.GetChargepointModel(input);
            var model = new ChargepointModelsViewModel(output);
            
            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditChargepointModelModal(EntityDto<int> input)
        {
            var output = await _chargepointModelAppService.GetChargepointModelForEdit(input);
            var viewModel = new CreateOrEditChargepointModelViewModel(output);

            ViewBag.Vendors = new SelectList(_vendorAppService.GetVendorsList().Vendors, "Id", "Name");
            ViewBag.MountTypes = new SelectList(_mountTypeAppService.GetMountTypesList().MountTypes, "Id", "Name");
            ViewBag.OCPPVersions = new SelectList(_ocppVersionAppService.GetOCPPVersionsList().OCPPVersions, "Id", "Name");
            ViewBag.OCPPTransports = new SelectList(_ocppTransportAppService.GetOCPPTransportsList().OCPPTransports, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}