using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ComOptions;
using Sixoclock.Onyx.Web.Areas.App.Models.ComOptions;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ComOptions.Dto;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ComOptionsController : OnyxControllerBase
    {
        private readonly IComOptionAppService _comOptionAppService;
        
        public ComOptionsController(IComOptionAppService _comOptionAppService)
        {
            this._comOptionAppService = _comOptionAppService;
        }
        public async Task<ActionResult> Index(GetComOptionInput input)
        {
            var output = await _comOptionAppService.GetComOption(input);
            var model = new ComOptionsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditComOptionModal(EntityDto<int> input)
        {
            var output = await _comOptionAppService.GetComOptionForEdit(input);
            var viewModel = new CreateOrEditComOptionViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}