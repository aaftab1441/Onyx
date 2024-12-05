using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Vendors;
using Sixoclock.Onyx.Vendors.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Vendors;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class VendorsController : OnyxControllerBase
    {
        private readonly IVendorAppService _vendorAppService;

        public VendorsController(IVendorAppService vendorAppService)
        {
            _vendorAppService = vendorAppService;
        }
        public async Task<ActionResult> Index(GetVendorInput input)
        {
            var output = await _vendorAppService.GetVendor(input);
            var model = new VendorsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditVendorModal(EntityDto<int> input)
        {
            var output = await _vendorAppService.GetVendorForEdit(input);
            var viewModel = new CreateOrEditVendorViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}