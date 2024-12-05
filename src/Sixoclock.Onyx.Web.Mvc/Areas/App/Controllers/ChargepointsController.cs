using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Chargepoints;
using Sixoclock.Onyx.Chargepoints.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Chargepoints;
using Sixoclock.Onyx.Groups;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.MountTypes;
using Sixoclock.Onyx.ChargePointModels;
using Sixoclock.Onyx.AdminStatuses;
using Sixoclock.Onyx.OCPPTransports;
using Sixoclock.Onyx.OCPPVersions;
using Sixoclock.Onyx.RestTypes;
using Sixoclock.Onyx.EVSEs;
using Sixoclock.Onyx.AvailabilityTypes;
using System.Threading.Tasks;
using Sixoclock.Onyx.Services;
using Sixoclock.Onyx.Authorization;
using System;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ChargepointsController : OnyxControllerBase
    {
        private readonly IChargepointAppService _chargepointAppService;
        private readonly IGroupAppService _groupAppService;
        private readonly IMountTypeAppService _mountTypeAppService;
        private readonly IChargePointModelAppService _chargepointModelAppService;
        private readonly IAdminStatusAppService _adminStatusAppService;
        private readonly IOCPPVersionAppService _ocppVersionAppService;
        private readonly IOCPPTransportAppService _ocppTransportAppService;
        private readonly IRestTypeAppService _resetTypeAppService;
        private readonly IEVSEAppService _eVSEAppService;
        private readonly IAvailabilityTypeAppService _availabilityTypeAppService;
        private readonly IChargepointServiceAppService _chargepointServiceAppService;

        public ChargepointsController(IChargepointAppService chargepointAppService, IGroupAppService groupAppService, 
            IMountTypeAppService mountTypeAppService, IChargePointModelAppService chargepointModelAppService, 
            IAdminStatusAppService adminStatusAppService,IOCPPVersionAppService oCPPVersionAppService, 
            IOCPPTransportAppService oCPPTransportAppService,IRestTypeAppService resetTypeAppService,
            IEVSEAppService eVSEAppService,IAvailabilityTypeAppService availabilityTypeAppService,
            IChargepointServiceAppService chargepointServiceAppService)
        {
            _chargepointAppService = chargepointAppService;
            _groupAppService = groupAppService;
            _mountTypeAppService = mountTypeAppService;
            _chargepointModelAppService = chargepointModelAppService;
            _adminStatusAppService = adminStatusAppService;
            _ocppVersionAppService = oCPPVersionAppService;
            _ocppTransportAppService = oCPPTransportAppService;
            _resetTypeAppService = resetTypeAppService;
            _eVSEAppService = eVSEAppService;
            _availabilityTypeAppService = availabilityTypeAppService;
            _chargepointServiceAppService = chargepointServiceAppService;
        }
        public async Task<ActionResult> Index(GetChargepointInput input)
        {
            var output = await _chargepointAppService.GetChargepoint(input);
            var model = new ChargepointsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditChargepointModal(EntityDto<int> input)
        {
            var output = _chargepointAppService.GetChargepointForEdit(input);
            var viewModel = new CreateOrEditChargepointViewModel(output);

            ViewBag.Groups = new SelectList( (await _groupAppService.GetGroupsList()).Groups, "Id", "Name");
            ViewBag.Mounts = new SelectList(_mountTypeAppService.GetMountTypesList().MountTypes, "Id", "Name");
            ViewBag.Models = new SelectList(_chargepointModelAppService.GetChargepointModelsList().ChargepointModels, "Id", "Name");
            ViewBag.AdminStatuses = new SelectList(_adminStatusAppService.GetAdminStatusesList().AdminStatuses, "Id", "Name");
            ViewBag.OCPPVersions = new SelectList(_ocppVersionAppService.GetOCPPVersionsList().OCPPVersions, "Id", "Name");
            ViewBag.OCPPTransports = new SelectList(_ocppTransportAppService.GetOCPPTransportsList().OCPPTransports, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
        public async Task<PartialViewResult> ManageChargepointModal(EntityDto<int> input)
        {
            var output = await _chargepointAppService.GetChargepointByIdForManageChargepoint(input);
            var model = new CreateOrEditChargepointViewModel(output);

            ViewBag.ResetTypes = new SelectList(_resetTypeAppService.GetRestTypesList().RestTypes, "Id", "Name");
            ViewBag.EVSEs = new SelectList((await _eVSEAppService.GetEVSEsListByChargepoint(input)).EVSEs, "Id", "EVSE_id");
            ViewBag.AvailabilityTypes = new SelectList(_availabilityTypeAppService.GetAvailabilityTypesList().AvailabilityTypes, "Id", "Name");

            return PartialView("_ManageChargepointModal",model);
        }
        public async Task<PartialViewResult> OverviewModal(EntityDto<int> input)
        {
            var output =await  _chargepointAppService.GetChargepointByIdForManageChargepoint(input);
            var model = new CreateOrEditChargepointViewModel(output);
            
            return PartialView("_OverviewModal",model);
        }
        public async Task<PartialViewResult> ManageOCPPModal(EntityDto<int> input)
        {
            var output = await _chargepointAppService.GetChargepointByIdForManageChargepoint(input);
            var model = new CreateOrEditChargepointViewModel(output);
            
            return PartialView("_ManageOCPPModal",model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult ChargepointServiceModel(int id)
        {
            ViewBag.ChargepointId = id;
            return PartialView("_ChargepointService");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditChargepointServiceModal(string id, string serviceId)
        {
            var output = await _chargepointServiceAppService.GetChargepointServiceForEdit(
                new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditChargepointServiceViewModel(output);

            return PartialView("_CreateOrEditChargepointServiceModal", viewModel);

        }
    }
}