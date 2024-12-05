using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Controllers;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Areas.App.Models.Customer;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Segments;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Countries;
using Sixoclock.Onyx.Security;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.Services;
using Sixoclock.Onyx.Web.Areas.App.Models.TagTransactions;
using Sixoclock.Onyx.Customers;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Services;
using System;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{

    [Area("App")]
    [AbpMvcAuthorize]
    public class CustomerController : OnyxControllerBase
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly ISegmentAppService _segmentAppService;
        private readonly ICountryAppService _countryAppService;
        private readonly IPasswordComplexitySettingStore _passwordComplexitySettingStore;
        private readonly ITagTransactionAppService _tagTransactionAppService;
        private readonly ICustomerServiceAppService _customerServiceAppService;
        private readonly ICustomerServicePriceParameterAppService _customerServicePriceParameterAppService;


        public CustomerController(ICustomerAppService customerAppService,
            ISegmentAppService segmentAppService, 
            ICountryAppService countryAppService, 
            IPasswordComplexitySettingStore passwordComplexitySettingStore,
            ITagTransactionAppService tagTransactionAppService,
            ICustomerServiceAppService customerServiceAppService,
            ICustomerServicePriceParameterAppService customerServicePriceParameterAppService)

        {
            _customerAppService = customerAppService;
            _segmentAppService = segmentAppService;
            _countryAppService = countryAppService;
            _passwordComplexitySettingStore = passwordComplexitySettingStore;
            _tagTransactionAppService = tagTransactionAppService;
            _customerServiceAppService = customerServiceAppService;
            _customerServicePriceParameterAppService = customerServicePriceParameterAppService;

        }

        public async Task<ActionResult> Index(GetCustomerInput input)
        {
            var output = await _customerAppService.GetCustomer(input);
            var model = new IndexViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateCustomerModal(EntityDto<int> input)
        {
            var output = await _customerAppService.GetCustomerForEdit(input);
            var viewModel = new CreateOrEditCustomerViewModel(output)
            {
                PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync()
            };
            var segmentlist = await _segmentAppService.GetSegmentsList();
            ViewBag.Segments = new SelectList(segmentlist.Segments, "Id", "Name");
            ViewBag.Countries = new SelectList(_countryAppService.GetCountriesList().Countries, "Id", "Name");

            return PartialView("_CreateCustomerModal", viewModel);
        }
        public PartialViewResult CustomerDashboardModal(EntityDto<int> input)
        {
            var output = _tagTransactionAppService.GetTransactionsDashboardByCustomer(input);
            var viewModel = new DashboardViewModel(output);
            ViewBag.DashBoard = "Customer : Private";
            return PartialView("../TopologyDashboard/_Dashboard", viewModel);
        }
        public PartialViewResult CustomerUtilisationModal(EntityDto<int> input)
        {
            ViewBag.Utilisation = "Customer : Private";
            ViewBag.Id = input.Id;
            return PartialView("../TopologyUtilisation/_Utilisation");
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult CustomerServiceModel(int id)
        {
            ViewBag.CustomerId = id;
            return PartialView("_CustomerService");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditCustomerServiceModal(string id, int customerId)
        {
            var output = await _customerServiceAppService.GetCustomerServiceForEdit(
                new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditCustomerServiceViewModel(output);
            ViewBag.CustomerId = customerId;
            return PartialView("_CreateOrEditCustomerServiceModal", viewModel);

        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult CustomerServicePriceParameterModel(int id)
        {
            ViewBag.CustomerServiceId = id;
            return PartialView("_PriceParameters");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditCustomerServicePriceParameterModal(string id, string serviceId)
        {
            var output = await _customerServicePriceParameterAppService.GeServicePriceParameterForEdit(new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id), ServiceId = Convert.ToInt32(serviceId) });
            var viewModel = new CreateOrEditServicesPriceParameterModalViewModel(output);

            return PartialView("CreateOrEditCustomerServicePriceParameterModal", viewModel);

        }
    }
}