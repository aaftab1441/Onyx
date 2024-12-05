using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Tags;
using Sixoclock.Onyx.Tags.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Tags;
using Sixoclock.Onyx.ParentTags;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Markets;
using Sixoclock.Onyx.Regions;
using Sixoclock.Onyx.Installs;
using Sixoclock.Onyx.AuthorizationStatuses;
using Sixoclock.Onyx.Groups;
using Sixoclock.Onyx.Customers;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class TagsController : OnyxControllerBase
    {
        private readonly ITagAppService _tagAppService;
        private readonly IParentTagAppService _parentTagAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly IMarketAppService _marketAppService;
        private readonly IRegionAppService _regionAppService;
        private readonly IInstallAppService _installAppService;
        private readonly IGroupAppService _groupAppService;
        private readonly IAuthorizationStatusAppService _authorizationStatusAppService; 

        public TagsController(ITagAppService tagAppService,IParentTagAppService parentTagAppService,
            ICustomerAppService customerAppService,IMarketAppService marketAppService,
            IRegionAppService regionAppService,IInstallAppService installAppService,
            IAuthorizationStatusAppService authorizationStatusAppService,IGroupAppService groupAppService)
        {
            _tagAppService = tagAppService;
            _parentTagAppService = parentTagAppService;
            _customerAppService = customerAppService;
            _marketAppService = marketAppService;
            _regionAppService = regionAppService;
            _installAppService = installAppService;
            _groupAppService = groupAppService;
            _authorizationStatusAppService = authorizationStatusAppService;
        }
        public async Task<ActionResult> Index(GetTagInput input)
        {
            var output = await _tagAppService.GetTag(input);
            var model = new TagsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditTagModal(EntityDto<int> input)
        {
            var output = await _tagAppService.GetTagForEdit(input);
            var viewModel = new CreateOrEditTagViewModel(output);
            var customers =await  _customerAppService.GetCustomersList();
            ViewBag.ParentTags = new SelectList(_parentTagAppService.GetParentTagsList().ParentTags, "Id", "Name");
            ViewBag.Customers = new SelectList(customers, "Id", "Name");
            var markets = await _marketAppService.GetMarketsList();
            ViewBag.Markets = new SelectList(markets.Markets, "Id", "Name");
            ViewBag.Regions = new SelectList((await _regionAppService.GetRegionsList()).Regions, "Id", "Name");
            ViewBag.Installations = new SelectList((await _installAppService.GetInstallsList()).Installs, "Id", "Name");
            ViewBag.Groups = new SelectList((await _groupAppService.GetGroupsList()).Groups, "Id", "Name");
            ViewBag.AuthorizationStatuses = new SelectList(_authorizationStatusAppService.GetAuthorizationStatusesList().AuthorizationStatuses, "Id", "Name");

            return PartialView("_CreateOrEditModal", viewModel);
        }
        public PartialViewResult ViewDetailsTagModal(EntityDto<int> input)
        {
            var output = _tagAppService.GetTagForViewDetails(input);
            var viewModel = new TagViewDetails(output);

            return PartialView("_ViewDetails", viewModel);
        }
    }
}