using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ModelKeyValues;
using Sixoclock.Onyx.ModelKeyValues.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ModelOCPPConfigs;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Vendors;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.ChargePointModels;
//using Sixoclock.Onyx.ModelConnectors;
using Sixoclock.Onyx.OCPPVersions;
using Sixoclock.Onyx.OCPPFeatures;
using Sixoclock.Onyx.ModelConnectors;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ModelOCPPConfigsController : OnyxControllerBase
    {
        private readonly IModelKeyValueAppService _modelKeyValueAppService;
        private readonly IVendorAppService _vendorAppService;
        private readonly IChargePointModelAppService _modelAppService;
        private readonly IModelConnectorAppService _modelConnectorAppService;
        private readonly IOCPPVersionAppService _oCPPVersionAppService;
        private readonly IOCPPFeatureAppService _oCPPFeatureAppService;

        public ModelOCPPConfigsController(IModelKeyValueAppService modelKeyValueAppService, IVendorAppService vendorAppService, IChargePointModelAppService modelAppService,
            IModelConnectorAppService modelConnectorAppService, IOCPPVersionAppService oCPPVersionAppService, IOCPPFeatureAppService oCPPFeatureAppService)
        {
            _modelKeyValueAppService = modelKeyValueAppService;
            _vendorAppService = vendorAppService;
            _modelAppService = modelAppService;
            _modelConnectorAppService = modelConnectorAppService;
            _oCPPVersionAppService = oCPPVersionAppService;
            _oCPPFeatureAppService = oCPPFeatureAppService;
        }
        public async Task<ActionResult> Index(GetModelKeyValueInput input)
        {
            var output = await _modelKeyValueAppService.GetModelKeyValue(input);
            var model = new ModelKeyValuesViewModel(output);

            ViewBag.Vendors = new SelectList(_vendorAppService.GetVendorsList().Vendors, "Id", "Name");
            ViewBag.Models = new SelectList(_modelAppService.GetChargepointModelsList().ChargepointModels, "Id", "Name");
            ViewBag.Connectors = new SelectList(_modelConnectorAppService.GetModelConnectorsList().ModelConnectors, "Id", "Name");
            ViewBag.OCPPVersions = new SelectList(_oCPPVersionAppService.GetOCPPVersionsList().OCPPVersions, "Id", "Name");
            ViewBag.Features = new SelectList(_oCPPFeatureAppService.GetOCPPFeaturesList().OCPPFeatures, "Id", "Name");

            return View(model);
        }
        public async Task<PartialViewResult> EditModelKeyValueModal(EntityDto<int> input)
        {
            var output = await _modelKeyValueAppService.GetModelKeyValueForEdit(input);
            var viewModel = new EditModelKeyValueViewModel(output);

            return PartialView("_EditModal", viewModel);
        }
    }
}