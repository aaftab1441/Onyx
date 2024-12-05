using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ParentTags;
using Sixoclock.Onyx.ParentTags.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ParentTags;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ParentTagsController : OnyxControllerBase
    {
        private readonly IParentTagAppService _parentTagAppService;
        
        public ParentTagsController(IParentTagAppService parentTagAppService)
        {
            _parentTagAppService = parentTagAppService;
        }
        public async Task<ActionResult> Index(GetParentTagInput input)
        {
            var output = await _parentTagAppService.GetParentTag(input);
            var model = new ParentTagsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditParentTagModal(EntityDto<int> input)
        {
            var output = await _parentTagAppService.GetParentTagForEdit(input);
            var viewModel = new CreateOrEditParentTagViewModel(output);
            
            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}