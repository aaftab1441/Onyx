using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ChargepointModelImages;
using Sixoclock.Onyx.ChargepointModelImages.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ChargepointModelImages;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ChargepointModelImagesController : OnyxControllerBase
    {
        private readonly IChargepointModelImageAppService _chargepointModelImageAppService;
        public ChargepointModelImagesController(IChargepointModelImageAppService chargepointModelImageAppService)
        {
            _chargepointModelImageAppService = chargepointModelImageAppService;
        }
        public async Task<ActionResult> Index(GetChargepointModelImageInput input)
        {
            var output = await _chargepointModelImageAppService.GetChargepointModelImage(input);
            var model = new ChargepointModelImagesViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditChargepointModelImageModal(EntityDto<int> input)
        {
            var output = await _chargepointModelImageAppService.GetChargepointModelImageForEdit(input);
            var viewModel = new CreateOrEditChargepointModelImageViewModel(output);
            
            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}