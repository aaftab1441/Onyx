using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class WelcomeController : OnyxControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}