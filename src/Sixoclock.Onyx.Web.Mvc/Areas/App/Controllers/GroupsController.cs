using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Groups;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Installs;
using Sixoclock.Onyx.Groups.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Groups;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Countries;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.Services;

using Sixoclock.Onyx.Services;
using Sixoclock.Onyx.Authorization;
using System;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{

    [Area("App")]
    [AbpMvcAuthorize]
    public class GroupsController : OnyxControllerBase
    {
        private readonly IGroupAppService _groupAppService;
        private readonly IInstallAppService _installAppService;
        private readonly ICountryAppService _countryAppService;
        private readonly ITagTransactionAppService _tagTransactionAppService;
        private readonly IGroupServiceAppService _groupServiceAppService;
        private readonly IGroupServicePriceParameterAppService _groupServicePriceParameterAppService;


        public GroupsController(IGroupAppService groupAppService,
            IInstallAppService installAppService,
            ICountryAppService countryAppService,
            ITagTransactionAppService tagTransactionAppService,
            IGroupServiceAppService groupServiceAppService,
            IGroupServicePriceParameterAppService groupServicePriceParameterAppService)
        {
            _groupAppService = groupAppService;
            _installAppService = installAppService;
            _countryAppService = countryAppService;
            _tagTransactionAppService = tagTransactionAppService;
            _groupServiceAppService = groupServiceAppService;
            _groupServicePriceParameterAppService = groupServicePriceParameterAppService;

        }
        public async Task<ActionResult> Index(GetGroupInput input)
        {
            var output = await _groupAppService.GetGroup(input);
            var model = new GroupsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditGroupModal(EntityDto<int> input)
        {
            var output = await _groupAppService.GetGroupForEdit(input);
            var viewModel = new CreateOrEditGroupViewModel(output);

            ViewBag.Installs = new SelectList((await _installAppService.GetInstallsList()).Installs, "Id", "Name");
            ViewBag.Countries = new SelectList(_countryAppService.GetCountriesList().Countries, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
        public PartialViewResult GroupDashboardModal(EntityDto<int> input)
        {
            var output = _tagTransactionAppService.GetTransactionsDashboardByGroup(input);
            var viewModel = new DashboardViewModel(output);
            ViewBag.DashBoard = "Group : Private";
            return PartialView("../TopologyDashboard/_Dashboard", viewModel);
        }
        public PartialViewResult GroupUtilisationModal(EntityDto<int> input)
        {
            ViewBag.Utilisation = "Group : Private";
            ViewBag.Id = input.Id;
            return PartialView("../TopologyUtilisation/_Utilisation");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult GroupServiceModel(int id)
        {
            ViewBag.GroupId = id;
            return PartialView("_GroupService");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditGroupServiceModal(string id, int groupId)
        {
            var output = await _groupServiceAppService.GetGroupServiceForEdit(
                new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditGroupServiceViewModel(output);
            ViewBag.GroupId = groupId;
            return PartialView("_CreateOrEditGroupServiceModal", viewModel);

        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult GroupServicePriceParameterModel(int id)
        {
            ViewBag.GroupServiceId = id;
            return PartialView("_PriceParameters");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditGroupServicePriceParameterModal(string id, string serviceId)
        {
            var output = await _groupServicePriceParameterAppService.GeServicePriceParameterForEdit(new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id), ServiceId = Convert.ToInt32(serviceId) });
            var viewModel = new CreateOrEditServicesPriceParameterModalViewModel(output);

            return PartialView("CreateOrEditGroupServicePriceParameterModal", viewModel);


        }
    }
}