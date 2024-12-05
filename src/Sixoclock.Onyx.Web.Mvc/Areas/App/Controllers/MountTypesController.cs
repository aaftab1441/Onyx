using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.MountTypes;
using Sixoclock.Onyx.MountTypes.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.MountTypes;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class MountTypesController : OnyxControllerBase
    {
        private readonly IMountTypeAppService _mountTypeAppService;

        public MountTypesController(IMountTypeAppService mountTypeAppService)
        {
            _mountTypeAppService = mountTypeAppService;
        }
        public async Task<ActionResult> Index(GetMountTypeInput input)
        {
            var output = await _mountTypeAppService.GetMountType(input);
            var model = new MountTypesViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditMountTypeModal(EntityDto<int> input)
        {
            var output = await _mountTypeAppService.GetMountTypeForEdit(input);
            var viewModel = new CreateOrEditMountTypeViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}