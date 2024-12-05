using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Areas.App.Models.Common.Modals;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class CommonController : OnyxControllerBase
    {
        public PartialViewResult LookupModal(LookupModalViewModel model)
        {
            return PartialView("Modals/_LookupModal", model);
        }
    }
}