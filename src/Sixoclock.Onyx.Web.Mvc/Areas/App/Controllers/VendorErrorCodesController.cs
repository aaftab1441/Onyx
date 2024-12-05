using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Vendors;
using Sixoclock.Onyx.VendorErrorCodes;
using Sixoclock.Onyx.VendorErrorCodes.Dto;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Web.Areas.App.Models.VendorErrorCodes;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class VendorErrorCodesController : OnyxControllerBase
    {
        private readonly IVendorAppService _vendorAppService;
        private readonly IVendorErrorCodeAppService _vendorErrorCodeAppService;

        public VendorErrorCodesController(IVendorAppService vendorAppService, IVendorErrorCodeAppService vendorErrorCodeAppService)
        {
            _vendorAppService = vendorAppService;
            _vendorErrorCodeAppService = vendorErrorCodeAppService;
        }
        public async Task<ActionResult> Index(GetVendorErrorCodeInput input)
        {
            var output = await _vendorErrorCodeAppService.GetVendorErrorCode(input);
            var model = new VendorErrorCodeViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditVendorErrorCodeModal(EntityDto<int> input)
        {
            var output = await _vendorErrorCodeAppService.GetVendorErrorCodeForEdit(input);
            var viewModel = new CreateOrEditVendorErrorCodeViewModel(output);

            ViewBag.Vendors = new SelectList(_vendorAppService.GetVendorsList().Vendors, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}