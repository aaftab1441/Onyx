using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Web.Areas.App.Models.Common.Modals;
using Sixoclock.Onyx.Web.Areas.App.Models.OrganizationUnits;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
    public class OrganizationUnitsController : OnyxControllerBase
    {
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;

        public OrganizationUnitsController(IRepository<OrganizationUnit, long> organizationUnitRepository)
        {
            _organizationUnitRepository = organizationUnitRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public PartialViewResult CreateModal(long? parentId)
        {
            return PartialView("_CreateModal", new CreateOrganizationUnitModalViewModel(parentId));
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);
            var model = ObjectMapper.Map<EditOrganizationUnitModalViewModel>(organizationUnit);

            return PartialView("_EditModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public PartialViewResult AddMemberModal(LookupModalViewModel model)
        {
            return PartialView("_AddMemberModal", model);
        }
    }
}