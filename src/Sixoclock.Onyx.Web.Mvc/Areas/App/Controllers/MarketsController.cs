using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Markets;
using Sixoclock.Onyx.Markets.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Markets;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.Services;
using Sixoclock.Onyx.Customers;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Services;
using System;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class MarketsController : OnyxControllerBase
    {
        private readonly IMarketAppService _marketAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly ITagTransactionAppService _tagTransactionAppService;
        private readonly IMarketServiceAppService _marketServiceAppService;
        private readonly IMarketServicePriceParameterAppService _marketServicePriceParameterAppService;


        public MarketsController(IMarketAppService marketAppService,
            ICustomerAppService customerAppService,
            ITagTransactionAppService tagTransactionAppService,
            IMarketServiceAppService marketServiceAppService, IMarketServicePriceParameterAppService marketServicePriceParameterAppService)
        {
            _marketAppService = marketAppService;
            _customerAppService = customerAppService;
            _tagTransactionAppService = tagTransactionAppService;
            _marketServiceAppService = marketServiceAppService;
            _marketServicePriceParameterAppService = marketServicePriceParameterAppService;

        }
        public async Task<ActionResult> Index(GetMarketInput input)
        {
            var output = await _marketAppService.GetMarket(input);
            var model = new MarketsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditMarketModal(EntityDto<int> input)
        {
            var output = await _marketAppService.GetMarketForEdit(input);
            var viewModel = new CreateOrEditMarketViewModel(output);
            var customers = await _customerAppService.GetCustomersList();
            ViewBag.Customers = new SelectList(customers, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
        public PartialViewResult MarketDashboardModal(EntityDto<int> input)
        {
            var output = _tagTransactionAppService.GetTransactionsDashboardByMarket(input);
            var viewModel = new DashboardViewModel(output);
            ViewBag.DashBoard = "Market : Private";
            return PartialView("../TopologyDashboard/_Dashboard", viewModel);
        }
        public PartialViewResult MarketUtilisationModal(EntityDto<int> input)
        {
            ViewBag.Utilisation = "Market : Private";
            ViewBag.Id = input.Id;
            return PartialView("../TopologyUtilisation/_Utilisation");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult MarketServiceModel(int id)
        {
            ViewBag.MarketId = id;
            return PartialView("_MarketService");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditMarketServiceModal(string id, int marketId)
        {
            var output = await _marketServiceAppService.GetMarketServiceForEdit(
                new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditMarketServiceViewModel(output); ViewBag.MarketId = marketId;
            return PartialView("_CreateOrEditMarketServiceModal", viewModel);

        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult MarketServicePriceParameterModel(int id)
        {
            ViewBag.MarketServiceId = id;
            return PartialView("_PriceParameters");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditMarketServicePriceParameterModal(string id, string serviceId)
        {
            var output = await _marketServicePriceParameterAppService.GeServicePriceParameterForEdit(new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id), ServiceId = Convert.ToInt32(serviceId) });
            var viewModel = new CreateOrEditServicesPriceParameterModalViewModel(output);

            return PartialView("CreateOrEditMarketServicePriceParameterModal", viewModel);

        }
    }
}