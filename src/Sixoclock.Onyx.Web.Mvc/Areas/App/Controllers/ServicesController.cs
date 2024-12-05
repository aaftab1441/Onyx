using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services;
using Sixoclock.Onyx.Web.Areas.App.Models.Services;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Services)]
    public class ServicesController : OnyxControllerBase
    {
        //Services
        private readonly IServicesAppService _servicesAppService;

        public ServicesController(IServicesAppService servicesAppService)
        {
            _servicesAppService = servicesAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            var output = await _servicesAppService.GetServiceForEdit(new EntityDto<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditServicesModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);

        }
    }
}