using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.AdminStatuses;
using Sixoclock.Onyx.AdminStatuses.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.AdminStatuses;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class AdminStatusesController : OnyxControllerBase
    {
        private readonly IAdminStatusAppService _adminStatusAppService;

        public AdminStatusesController(IAdminStatusAppService adminStatusAppService)
        {
            _adminStatusAppService = adminStatusAppService;
        }
        public async Task<ActionResult> Index(GetAdminStatusInput input)
        {
            var output = await _adminStatusAppService.GetAdminStatus(input);
            var model = new AdminStatusesViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditAdminStatusModal(EntityDto<int> input)
        {
            var output = await _adminStatusAppService.GetAdminStatusForEdit(input);
            var viewModel = new CreateOrEditAdminStatusViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}