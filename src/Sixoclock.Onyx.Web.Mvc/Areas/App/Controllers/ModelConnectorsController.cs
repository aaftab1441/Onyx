using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ModelConnectors;
using Sixoclock.Onyx.ModelConnectors.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ModelConnectors;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ChargePointModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Vendors;
using Sixoclock.Onyx.ConnectorTypes;
using Sixoclock.Onyx.ModelEVSEs;
using Sixoclock.Onyx.Capacities;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ModelConnectorsController : OnyxControllerBase
    {
        private readonly IModelConnectorAppService _modelConnectorAppService;
        private readonly IChargePointModelAppService _chargePointModelAppService;
        private readonly IModelEVSEAppService _modelEVSEAppService;
        private readonly IVendorAppService _vendorAppService;
        private readonly IConnectorTypeAppService _connectorTypeAppservice;
        private readonly ICapacityAppService _capacityAppService;

        public ModelConnectorsController(IModelConnectorAppService modelConnectorAppService, IChargePointModelAppService chargePointModelAppService, 
            IVendorAppService vendorAppService, IConnectorTypeAppService connectorTypeAppservice, IModelEVSEAppService modelEVSEAppService,ICapacityAppService capacityAppService)
        {
            _modelConnectorAppService = modelConnectorAppService;
            _chargePointModelAppService = chargePointModelAppService;
            _vendorAppService = vendorAppService;
            _connectorTypeAppservice = connectorTypeAppservice;
            _modelEVSEAppService = modelEVSEAppService;
            _capacityAppService = capacityAppService;
        }
        public async Task<ActionResult> Index(GetModelConnectorInput input)
        {
            var output = await _modelConnectorAppService.GetModelConnector(input);
            var model = new ModelConnectorsViewModel(output);

            return View(model);
        }
        public PartialViewResult CreateOrEditModelConnectorModal(EntityDto<int> input)
        {
            var output = _modelConnectorAppService.GetModelConnectorForEdit(input);
            var viewModel = new CreateOrEditModelConnectorViewModel(output);
            
            ViewBag.Vendors = new SelectList(_vendorAppService.GetVendorsList().Vendors, "Id", "Name");
            ViewBag.ChargepointModels = new SelectList(_chargePointModelAppService.GetChargepointModelsList().ChargepointModels, "Id", "Name");
            ViewBag.ModelEVSEs = new SelectList(_modelEVSEAppService.GetModelEVSEsList().ModelEVSEs, "Id", "EVSEId");
            ViewBag.ConnectorTypes = new SelectList(_connectorTypeAppservice.GetConnectorTypesList().ConnectorTypes, "Id", "Name");
            ViewBag.Capacities = new SelectList(_capacityAppService.GetCapacitiesList().Capacities, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}