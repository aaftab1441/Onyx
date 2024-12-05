using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Services;
using Sixoclock.Onyx.Web.Areas.App.Models.Services;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Services)]
    public class ServicePriceParametersController : OnyxControllerBase
    {
        private readonly IServicePriceParameterAppService _servicePriceParameterAppService;

        public ServicePriceParametersController(IServicePriceParameterAppService servicePriceParameterAppService)
        {
            _servicePriceParameterAppService = servicePriceParameterAppService;
        }
        public PartialViewResult Index(string id)
        {
            
            ViewBag.ServiceId = id;
            return PartialView();

        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id, string serviceId)
        {
            var output = await _servicePriceParameterAppService.GeServicePriceParameterForEdit(new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id), ServiceId = Convert.ToInt32(serviceId) });
            var viewModel = new CreateOrEditServicesPriceParameterModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);

        }
    }
}