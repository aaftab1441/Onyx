using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace Sixoclock.Onyx.Web.Controllers
{
    public class HomeController : OnyxControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
