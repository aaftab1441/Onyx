using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Connectors;
using Sixoclock.Onyx.Connectors.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Connectors;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ConnectorTypes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.MeterTypes;
using Sixoclock.Onyx.Capacities;
using Sixoclock.Onyx.OCPPTransports;
using Sixoclock.Onyx.OCPPVersions;
using Sixoclock.Onyx.EVSEs;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ConnectorsController : OnyxControllerBase
    {
        private readonly IConnectorAppService _connectorAppService;
        private readonly IConnectorTypeAppService _connectorTypeAppService;
        private readonly ICapacityAppService _capacityAppService;
        private readonly IEVSEAppService _iEVSEAppService;

        public ConnectorsController(IConnectorAppService connectorAppService, IConnectorTypeAppService connectorTypeAppService,
            ICapacityAppService capacityAppService, IEVSEAppService iEVSEAppService)
        {
            _connectorAppService = connectorAppService;
            _connectorTypeAppService = connectorTypeAppService;
            _capacityAppService = capacityAppService;
            _iEVSEAppService = iEVSEAppService;
        }
        public async Task<ActionResult> Index(GetConnectorInput input)
        {
            var output = await _connectorAppService.GetConnector(input);
            var model = new ConnectorsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditConnectorModal(EntityDto<int> input)
        {
            var output = _connectorAppService.GetConnectorForEdit(input);
            var viewModel = new CreateOrEditConnectorViewModel(output);

            ViewBag.ConnectorTypes = new SelectList(_connectorTypeAppService.GetConnectorTypesList().ConnectorTypes, "Id", "Name");
            ViewBag.Capacities = new SelectList(_capacityAppService.GetCapacitiesList().Capacities, "Id", "Name");
            ViewBag.EVSEs = new SelectList((await _iEVSEAppService.GetEVSEsList()).EVSEs, "Id", "EVSE_id");

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}