using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Markets;
using Sixoclock.Onyx.Regions;
using Sixoclock.Onyx.Regions.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Regions;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.Services;
using Sixoclock.Onyx.Authorization;
using System;
using Sixoclock.Onyx.Services;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class RegionsController : OnyxControllerBase
    {
        private readonly IMarketAppService _marketAppService;
        private readonly IRegionAppService _regionAppService;
        private readonly ITagTransactionAppService _tagTransactionAppService;
        private readonly IRegionServiceAppService _regionServiceAppService;
        private readonly IRegionServicePriceParameterAppService _regionServicePriceParameterAppService;



        public RegionsController(IMarketAppService marketAppService,
            IRegionAppService regionAppService,
            ITagTransactionAppService tagTransactionAppService,
            IRegionServiceAppService regionServiceAppService,
            IRegionServicePriceParameterAppService regionServicePriceParameterAppService)
        {
            _marketAppService = marketAppService;
            _regionAppService = regionAppService;
            _tagTransactionAppService = tagTransactionAppService;
            _regionServiceAppService = regionServiceAppService;
            _regionServicePriceParameterAppService = regionServicePriceParameterAppService;

        }
        public async Task<ActionResult> Index(GetRegionInput input)
        {
            var output = await _regionAppService.GetRegion(input);
            var model = new RegionsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditRegionModal(EntityDto<int> input)
        {
            var output = await _regionAppService.GetRegionForEdit(input);
            var viewModel = new CreateOrEditRegionViewModel(output);

            ViewBag.Markets = new SelectList((await _marketAppService.GetMarketsList()).Markets, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
        public PartialViewResult RegionDashboardModal(EntityDto<int> input)
        {
            var output = _tagTransactionAppService.GetTransactionsDashboardByRegion(input);
            var viewModel = new DashboardViewModel(output);
            ViewBag.DashBoard = "Region : Private";
            return PartialView("../TopologyDashboard/_Dashboard", viewModel);
        }
        public PartialViewResult RegionUtilisationModal(EntityDto<int> input)
        {
            ViewBag.Utilisation = "Region : Private";
            ViewBag.Id = input.Id;
            return PartialView("../TopologyUtilisation/_Utilisation");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult RegionServiceModel(int id)
        {
            ViewBag.RegionId = id;
            return PartialView("_RegionService");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditRegionServiceModal(string id, int regionId)
        {
            var output = await _regionServiceAppService.GetRegionServiceForEdit(
                new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditRegionServiceViewModel(output);

            ViewBag.RegionId = regionId;
            return PartialView("_CreateOrEditRegionServiceModal", viewModel);

        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult RegionServicePriceParameterModel(int id)
        {
            ViewBag.RegionServiceId = id;
            return PartialView("_PriceParameters");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditRegionServicePriceParameterModal(string id, string serviceId)
        {
            var output = await _regionServicePriceParameterAppService.GeServicePriceParameterForEdit(new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id), ServiceId = Convert.ToInt32(serviceId) });
            var viewModel = new CreateOrEditServicesPriceParameterModalViewModel(output);

            return PartialView("CreateOrEditRegionServicePriceParameterModal", viewModel);


        }

    }
}