using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Installs;
using Sixoclock.Onyx.Regions;
using Sixoclock.Onyx.Installs.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Installs;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Web.Areas.App.Models.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.Services;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.Authorization;
using System;
using Sixoclock.Onyx.Services;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class InstallsController : OnyxControllerBase
    {
        private readonly IInstallAppService _installAppService;
        private readonly IRegionAppService _regionAppService;
        private readonly ITagTransactionAppService _tagTransactionAppService;
        private readonly IInstallServiceAppService _installServiceAppService;
        private readonly IInstallServicePriceParameterAppService _installServicePriceParameterAppService;


        public InstallsController(IInstallAppService installAppService,
            IRegionAppService regionAppService,
            ITagTransactionAppService tagTransactionAppService,
            IInstallServiceAppService installServiceAppService,
            IInstallServicePriceParameterAppService installServicePriceParameterAppService)
        {
            _installAppService = installAppService;
            _regionAppService = regionAppService;
            _tagTransactionAppService = tagTransactionAppService;
            _installServiceAppService = installServiceAppService;
            _installServicePriceParameterAppService = installServicePriceParameterAppService;

        }
        public async Task<ActionResult> Index(GetInstallInput input)
        {
            var output = await _installAppService.GetInstall(input);
            var model = new InstallsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditInstallModal(EntityDto<int> input)
        {
            var output = await _installAppService.GetInstallForEdit(input);
            var viewModel = new CreateOrEditInstallViewModel(output);

            ViewBag.Regions = new SelectList((await _regionAppService.GetRegionsList()).Regions, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
        public PartialViewResult InstallDashboardModal(EntityDto<int> input)
        {
            var output = _tagTransactionAppService.GetTransactionsDashboardByInstall(input);
            var viewModel = new DashboardViewModel(output);
            ViewBag.DashBoard = "Install : Private";
            return PartialView("../TopologyDashboard/_Dashboard", viewModel);
        }
        public PartialViewResult InstallUtilisationModal(EntityDto<int> input)
        {
            ViewBag.Utilisation = "Install : Private";
            ViewBag.Id = input.Id;
            return PartialView("../TopologyUtilisation/_Utilisation");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult InstallServiceModel(int id)
        {
            ViewBag.InstallId = id;
            return PartialView("_InstallService");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditInstallServiceModal(string id, int installId)
        {
            var output = await _installServiceAppService.GetInstallServiceForEdit(
                new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditInstallServiceViewModel(output);

            ViewBag.InstallId = installId;
            return PartialView("_CreateOrEditInstallServiceModal", viewModel);

        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult InstallServicePriceParameterModel(int id)
        {
            ViewBag.InstallServiceId = id;
            return PartialView("_PriceParameters");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditInstallServicePriceParameterModal(string id, string serviceId)
        {
            var output = await _installServicePriceParameterAppService.GeServicePriceParameterForEdit(new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id), ServiceId = Convert.ToInt32(serviceId) });
            var viewModel = new CreateOrEditServicesPriceParameterModalViewModel(output);

            return PartialView("CreateOrEditInstallServicePriceParameterModal", viewModel);


        }
    }
}